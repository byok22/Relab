//#region  Imports
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, OnInit, signal, WritableSignal } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { GenericFormComponent } from '../../../shared/components/generic-form/generic-form.component';
import { GenericMenuComponent } from '../../../shared/components/generic-menu/generic-menu.component';
import { GenericTableComponent } from '../../../shared/components/generic-table/generic-table.component';
import { GenericTitleComponent } from '../../../shared/components/generic-title/generic-title.component';
import { PrimengModule } from '../../../shared/modules/primeng.module';
import { DatePipe } from '@angular/common';
import { DropdownsService } from '../../../common/service/dropdowns.service';
import { GenericMenuInterface } from '../../../shared/components/generic-menu-item/interfaces/generic-menu-item.interface';
import { SelectOption } from '../../../shared/interfaces/select-option.interface';
import { GenericMenuConcreteBuilder } from '../../../shared/components/generic-menu/builder/generic-menu-concret-builder';
import { GenericTableConfig } from '../../../shared/components/generic-table/interfaces/generic-table-config';
import { TableBuilderFactoryService } from '../../../shared/components/generic-table/service/table-builder-factory-service.service';
import { BasicKpi } from '../../../shared/interfaces/basic-kpi.interface';
import { TableColumn } from '../../../shared/components/generic-table/interfaces/table-column';
import { GenericFormInterface } from '../../../shared/components/generic-form/generic-form.interface';
import { GenericFormConcretBuilder } from '../../../shared/components/generic-form/builder/generic-form-concret-builder';
import { GenericStatus } from '../../../shared/enums/generic-status.enum';
import { FormBuilder } from '@angular/forms';
//import { UsersService } from '../../../users/services/users.service';
import { GenericPageTableMenuForm } from '../../../common/interfaces/generic-page-table-menu-form';
import { MessageService } from 'primeng/api';
import { EmployeesService } from '../../services/employees.service';
import { Employee } from '../../../shared/interfaces/employees/employee.interface';
import { EmployeeType } from '../../../shared/enums/employee-type.enum';

//#endregion Imports

//#region  Inits
@Component({
  selector: 'employees-page',
  standalone: true,
  imports: [
    CommonModule, GenericMenuComponent, GenericTableComponent, HttpClientModule, GenericTitleComponent, GenericFormComponent, PrimengModule, FontAwesomeModule
  ],
  providers: [DatePipe, DropdownsService, MessageService],
  templateUrl: './employees.page.component.html',
  styleUrl: './employees.page.component.css',
  changeDetection: ChangeDetectionStrategy.Default
})
export class EmployeesPageComponent implements OnInit, GenericPageTableMenuForm<Employee> {

  constructor(
    private service: EmployeesService,
    //private userService: UsersService,
    private serviceTable: TableBuilderFactoryService,
    private fb: FormBuilder,
    private _message: MessageService,
    private dropdownsService: DropdownsService
  ) {
    this.FillMenu();
    this.ConfigMenu();
  }

  dataForm: WritableSignal<Employee> = signal({
    id: 1,
    name: '', 
    employeeNumber: '',
    employeeType: EmployeeType.Engineer
  });

  dataFormTemp: WritableSignal<Employee> = signal({
    id: 1,
    name: '', 
    employeeNumber: '',
    employeeType: EmployeeType.Engineer
  });

  ngOnInit(): void {
    this.ConfigForm();
  }

  //#endregion Inits

  //#region  Variables
  //Builders
  builderTable = this.serviceTable.createBuilder<Employee>();
  //Menu
  menuItems: GenericMenuInterface[] = [];

  //Table
  tableConfig!: GenericTableConfig<Employee>;
  dataTable: Employee[] = [];
  hideTable = signal(true);
  public newTable = signal(true);
  /** Signal que contiene un Employee */
  public dataEmployee = signal<Employee>({
    id: 1,
    name: '', 
    employeeNumber: '',
    employeeType: EmployeeType.Engineer
  });

  public employeeTemp:Employee=({
    id: 1,
    name: '', 
    employeeNumber: '',
    employeeType: EmployeeType.Engineer
  });

  public dataEmployees = signal<Employee[]>([]);

  public EditAdd = signal<string>('');
  public displayMaximizable: boolean = false;

  //Form
  genericForm: GenericFormInterface<Employee> = {
    tittle: '',
    fields: [],
    customFromGroup: undefined,
    editAdd: '',
    data: this.dataEmployee()
  }

  testForm: GenericFormInterface<Employee> = {
    tittle: '',
    fields: [],
    customFromGroup: undefined,
    editAdd: '',
    data: this.employeeTemp
  };

  builderForm = new GenericFormConcretBuilder<Employee>();
  builderTestForm = new GenericFormConcretBuilder<Employee>();
  statuses: SelectOption[] = this.getEnumSelectOptions(EmployeeType);
  public submit = signal(false);

  //#endregion

  //#region  Menu
  statusDD: SelectOption[] = [];
  selectedStatus: string = '';

  statusItem: GenericMenuInterface = {
    item: {
      selectedOption: this.selectedStatus,
      options: this.statusDD, onChange: (event: string) => {
        this.selectedStatus = event;
        this.hideTable.set(true);
        console.log('Selected option changed:', event);
      }
    },
    labelText: 'Status',
    order: 1,
    type: 'dropdown'
  }

  FillMenu(): void {
    this.dropdownsService.getEmployeeTypes().subscribe({
      next: (status) => {
        this.statusDD = status;
        this.selectedStatus = status[0]?.id || '1'; // Asegúrate de que selectedBuilding tiene un valor válido
        this.statusItem.item.options = this.statusDD;
        this.statusItem.item.selectedOption = this.selectedStatus;
      },
      error: (error) => {
        console.error(error);
      }
    });
  }

  ConfigMenu(): void {
    const builder = new GenericMenuConcreteBuilder();
    builder.Reset();

    builder.SetDropDown(this.statusItem);

    builder.SetButton({
      item: {
        onClick: () => {
          this.GetTable(this.selectedStatus);
        }
      },
      labelText: 'Find',
      order: 5,
      type: 'button'
    });

    this.menuItems = builder.Generate();
  }
  //#endregion

  //#region  Table

  ConfigTable() {
    this.builderTable.Reset();
    this.builderTable.SetTitle("Employee Table");
    this.builderTable.SetDataKey("id");
    this.builderTable.SetData(this.dataTable);
    this.builderTable.SetKpis(this.GetKpis());
    this.builderTable.SetPagination(true);
    this.builderTable.SetRowsPerPage(10);
    this.builderTable.SetRowsPerPageOptions([5, 10, 20]);
    this.builderTable.SetColumns(this.getColumns());
    this.builderTable.SetGlobalFilterFields(["name"]);
    this.tableConfig = this.builderTable.Generate();
  }

  GetKpis(): BasicKpi[] {
    return [
      { title: "Total", total: this.dataTable.length.toString() },
    ];
  }

  getColumns(): TableColumn[] {
    const manualColumns: TableColumn[] = [
      { field: 'id', header: 'ID' },
      { field: 'employeeNumber', header: 'Employee Number' },      
      { field: 'name', header: 'Employee' },
      { field: 'employeeType', header: 'Employee Type' },
    ];

    const data = this.dataTable;
    const columnFields = Object.keys(data[0]);
    const manualFields = manualColumns.map(col => col.field);
    const filteredColumnFields = columnFields.filter(field => !manualFields.includes(field));

    const dataColumns: TableColumn[] = filteredColumnFields.map(field => ({
      field,
      header: this.capitalizeFirstLetter(field)
    }));

    let columns: TableColumn[] = [...manualColumns, ...dataColumns];
    const fieldsToHide = [""];

    columns = columns.map(column => ({
      ...column,
      showHeader: !fieldsToHide.includes(column.field)
    }));

    return columns;
  }

  capitalizeFirstLetter(word: string): string {
    if (!word) return word;
    return word[0].toUpperCase() + word.substr(1).toLowerCase();
  }

  GetTable(status: string | any, ...args: any[]): void {
    try {
      this.service.getEmployeesByStatus(status).subscribe({
        next: (employeeRequest) => {
          const transformedEmployeeRequest = employeeRequest.map(request => ({
            ...request,           
          }));

          this.dataTable = transformedEmployeeRequest;

          if (this.newTable()) {
            this.ConfigTable();
            this.newTable.set(false);
            this.hideTable.set(false);
            return;
          }
          this.ConfigTable();
          this.builderTable.SetData(this.dataTable);
          this.hideTable.set(false);
        },
        error: (error) => {
          console.error(error);
        },
        complete: () => { }
      });

    } catch (error) {
      console.error('Error fetching data', error);
      throw error;
    }
  }

  //#endregion

  //#region Form

  ConfigForm() {
    this.builderForm.Reset();
    this.builderForm.SetEditAdd(this.EditAdd().toString());
    this.employeeTemp=this.dataEmployee();

    this.builderForm.SetField({
      field: 'id',
      label: 'id',
      order: 1,
      required: false,
      type: 'text',
      validationRequired: false,
      enable: false,
      show: false,
      value: this.employeeTemp.id
    });

    this.builderForm.SetField({
      field: 'employeeNumber',
      label: 'Employee Number',
      order: 2,
      required: true,
      type: 'text',
      validationRequired: true,
      enable: true,
      show: true,
      value: this.employeeTemp.employeeNumber
    });

    this.builderForm.SetField({
      field: 'name',
      label: 'Employee Name',
      order: 3,
      required: true,
      type: 'text',
      validationRequired: true,
      enable: true,
      show: true,
      value: this.employeeTemp.name
    });

    const selectedStatus = this.statuses.find(status => status.text.toUpperCase() === this.employeeTemp.employeeType);
    this.builderForm.SetField({
      field: 'employeeType',
      label: 'Employee Type',
      order: 4,
      required: true,
      type: 'select',
      validationRequired: true,
      enable: true,
      show: true,
      value: selectedStatus?.id || '',
      options: this.statuses
    });

    this.builderForm.SetFormGroup(
      this.fb.group({
      id: this.employeeTemp.id,
      employeeNumber: [this.employeeTemp.employeeNumber],
      name: [this.employeeTemp.name],
      employeeType: [this.employeeTemp.employeeType],
      })
    );

    this.builderForm.SetSubmitFunction(() => {
      this.SubmitRequests();
    });

    this.builderForm.SetTitle('Employee Form');
    this.genericForm = this.builderForm.Generate();
  }

  //ADD OR EDIT
  SubmitRequests(): void {
    console.log('Se hizo Submit');
    console.log(this.dataEmployee());

    if (this.genericForm.customFromGroup && this.genericForm.customFromGroup.valid) {
      const formValues = this.genericForm.customFromGroup.value;
      const selectedStatus = this.statuses.find(status => status.id === formValues.employeeType);
      const selectedStatusText = selectedStatus?.text || '';
      const eltype = EmployeeType[selectedStatusText as keyof typeof EmployeeType] || EmployeeType.Engineer;

      this.dataEmployee.set({
        id: formValues.id || '',
        employeeNumber: formValues.employeeNumber,
        name: formValues.name,
        employeeType: eltype,
      });
      this.submit.set(true);
    }

    console.log('Se hizo Submit');
    console.log(this.dataEmployee());

    if (this.EditAdd() === 'Add') {
      this.service.createEmployee(this.dataEmployee()).subscribe({
        next: (response) => {
          this._message.add({
            severity: 'success',
            summary: 'Add!',
            detail: `Employee ${response.message} Added`,
            life: 2000
          });

          setTimeout(() => {
            this.GetTable(this.selectedStatus);
          }, 1000); // Delay of 1 second
        },
        error: () => {},
        complete: () => {}
      });
    } else {
      this.service.updateEmployee(this.dataEmployee()).subscribe({
        next: (response) => {
          this._message.add({
            severity: 'success',
            summary: 'Edit!',
            detail: `Employee ${response.message} Updated`,
            life: 2000
          });
          this.GetTable(this.selectedStatus);
        },
        error: () => {},
        complete: () => {}
      });
    }

    this.displayMaximizable = false;
    this.dataEmployees = signal<Employee[]>([]);
    console.log(this.genericForm.data);
  }

  getModal(item: Employee = {} as Employee) {
    this.submit.set(false);

    if (item.id == 0 || item.id == undefined) {
      this.EditAdd.set('Add')
    } else {
      this.EditAdd.set('Edit')
    }

    if (this.EditAdd() == 'Edit') {
      this.dataEmployee.set(item);
      this.ConfigForm();
      this.displayMaximizable = true;
      let employee: Employee;
      this.service.getEmployeeByNumber(item.employeeNumber).subscribe({
        next: (data) => {
          employee = data;
          this.dataEmployee.set(employee);
        },
        error: (error) => {
          console.error(error);
          this.ConfigForm();
        },
        complete: () => {
          this.ConfigForm();
        }
      });
    } else {
      const dataEmployeeTemp: Employee = {
        id: 1,
        employeeNumber: '',
        name: '',
        employeeType: EmployeeType.Engineer
      }

      this.dataEmployee.set(dataEmployeeTemp);
      this.ConfigForm();
      this.displayMaximizable = true;
    }
  }

  //#endregion

  //#region Extras

  parseDate(dateString: string): Date {
    const [datePart, timePart] = dateString.split(' ');
    const [day, month, year] = datePart.split('-').map(Number);
    const [hours, minutes, seconds] = timePart.split(':').map(Number);
    return new Date(year, month - 1, day, hours, minutes, seconds);
  }

  getEnumSelectOptions(enumType: any): SelectOption[] {
    return Object.values(enumType).map(value => ({
      id: this.generateRandomId(),
      text: value as string // Asegurando que el tipo de 'text' sea 'string'
    }));
  }

  generateRandomId(): string {
    return Math.random().toString(36).substr(2, 9); // Genera un id aleatorio
  }

  //#endregion
}