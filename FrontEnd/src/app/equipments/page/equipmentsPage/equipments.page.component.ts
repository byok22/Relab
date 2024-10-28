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
import { EquipmentService } from '../../services/equipment.service';
import { EquipmentDto } from '../../interfaces/equipment-dto';
import { FormBuilder } from '@angular/forms';
//import { UsersService } from '../../../users/services/users.service';
import { GenericPageTableMenuForm } from '../../../common/interfaces/generic-page-table-menu-form';
import { MessageService } from 'primeng/api';

//#endregion Imports





//#region  Inits
@Component({
  selector: 'equipments-page',
  standalone: true,
  imports: [
    CommonModule, GenericMenuComponent, GenericTableComponent, HttpClientModule, GenericTitleComponent, GenericFormComponent, PrimengModule, FontAwesomeModule
  ],
  providers: [DatePipe, DropdownsService, MessageService],
  templateUrl: './equipments.page.component.html',
  styleUrl: './equipments.page.component.css',
  changeDetection: ChangeDetectionStrategy.Default
})
export class EquipmentsPageComponent implements OnInit, GenericPageTableMenuForm<EquipmentDto> {

  /**
   *
   */
  constructor(
    private service: EquipmentService,
    //private userService: UsersService,
    private serviceTable: TableBuilderFactoryService,
    private datePipe: DatePipe,
    private fb: FormBuilder,
    private _message: MessageService 
  ) {
    this.FillMenu();
    this.ConfigMenu();
  }
  dataForm: WritableSignal<EquipmentDto>= signal(
    {calibrationDate:'',
      id:1,
      name:'',
      description:''
    }
  );
  dataFormTemp: WritableSignal<EquipmentDto>= signal(
    {calibrationDate:'',
      id:1,
      name:'',
       description:''
    }
  );
  ngOnInit(): void {

   
    this.ConfigForm();
  }

  //#endregion Inits





  //#region  Variables
  //Builders
  builderTable = this.serviceTable.createBuilder<EquipmentDto>();
  //Menu
  menuItems: GenericMenuInterface[] = [];

  //Table
  tableConfig!: GenericTableConfig<EquipmentDto>;
  dataTable: EquipmentDto[] = [];
  hideTable = signal(true);
  public newTable = signal(true);
  /** Signal que contiene un EquipmentDto */
  public dataEquipment = signal<EquipmentDto>
    (
      {
        id: 1,
        calibrationDate: '',
        name: '',
        description: ''
      }
    );

  public equipmentTemp = signal<EquipmentDto>
    (
      {
        id: 1,
        calibrationDate: '',
        name: '',
        description: ''
      }
    );
  public dataEquipments = signal<EquipmentDto[]>
    (
      []
    );

  public EditAdd = signal<string>('');
  public displayMaximizable: boolean = false;


  //Form
  genericForm: GenericFormInterface<EquipmentDto> = {
    tittle: '',
    fields: [],
    customFromGroup: undefined,
    editAdd: '',
    data: this.dataEquipment()
  }

  testForm: GenericFormInterface<EquipmentDto> = {
    tittle: '',
    fields: [],
    customFromGroup: undefined,
    editAdd: '',
    data: this.equipmentTemp()
  };
  builderForm = new GenericFormConcretBuilder<EquipmentDto>();
  builderTestForm = new GenericFormConcretBuilder<EquipmentDto>();
  statuses: SelectOption[] = this.getEnumSelectOptions(GenericStatus);
  /*usersSelect: SelectOption = {
    id: '',
    text: ''
  }*/
  //usersSelects: SelectOption[] = [];
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
    this.service.getStatus().subscribe({
      next: (status) => {
        this.statusDD = status;
        this.selectedStatus = status[0]?.id || '1'; // Asegúrate de que selectedBuilding tiene un valor válido
        this.statusItem.item.options = this.statusDD;
        this.statusItem.item.selectedOption = this.selectedStatus;
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
    this.builderTable.SetTitle("Equipment Table");
    this.builderTable.SetDataKey("id");
    this.builderTable.SetData(this.dataTable);
    this.builderTable.SetKpis(this.GetKpis());
    this.builderTable.SetPagination(true);
    this.builderTable.SetRowsPerPage(10);
    this.builderTable.SetRowsPerPageOptions([5, 10, 20]);
    this.builderTable.SetColumns(this.getColumns());
    this.builderTable.SetGlobalFilterFields(["name"]);
    //builder.SetGroupBy("category");
    // builder.Generate();

    this.tableConfig = this.builderTable.Generate();
  }

  GetKpis(): BasicKpi[] {

    return [
      { title: "Total", total: this.dataTable.length.toString() },
    ];
  }


  //Function to get all columns and hide some fields
  getColumns(): TableColumn[] {
    // Definir columnas manualmente
    const manualColumns: TableColumn[] = [
      { field: 'id', header: 'ID' },
      { field: 'name', header: 'Equipment' },
      { field: 'calibrationDate', header: 'Calibration Date' },
      //{ field: 'employeeAccount', header: 'Employee Account' },
    //  { field: 'userName', header: 'Name' },

    ];

    // Obtener campos de datos
    const data = this.dataTable;
    const columnFields = Object.keys(data[0]);

    // Eliminar campos que ya están definidos manualmente
    const manualFields = manualColumns.map(col => col.field);
    const filteredColumnFields = columnFields.filter(field => !manualFields.includes(field));

    // Crear columnas a partir de los campos de datos restantes
    const dataColumns: TableColumn[] = filteredColumnFields.map(field => ({
      field,
      header: this.capitalizeFirstLetter(field)
    }));

    // Concatenar columnas manuales con columnas de datos
    let columns: TableColumn[] = [...manualColumns, ...dataColumns];

    // Campos para ocultar
    const fieldsToHide = [""];

    // Agregar propiedad showHeader para ocultar columnas
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

  //GetTable(status: string): void;
  // GetTable(...args: any[]): void;
  GetTable(status: string | any, ...args: any[]): void {

    try {
      this.service.getEquipmentsByStatus(status).subscribe({
        next: (equipmentRequest) => {
          const transformedEquipmentRequest = equipmentRequest.map(request => ({
            ...request,
            calibrationDate: this.datePipe.transform(request.calibrationDate, 'dd-MM-yyyy HH:mm:ss') ?? ''
          }));

          this.dataTable = transformedEquipmentRequest;

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

    /*try {
      await this.userService.getUsersByType('Eng').subscribe({
        next: (response) => {
          this.usersSelects = response;
        },
        error: (error) => {
          console.log(error);
        }
      });

    } catch (erro) {
      console.log(erro);
    }*/



    const calibrationDateStr: string = this.dataEquipment().calibrationDate ?? '';
    const calibrationDAte = calibrationDateStr != '' ? this.parseDate(calibrationDateStr) : '';

    const dateYYYMMDDCalibration = calibrationDAte != '' ? this.formatDateToYYYYMMDD(calibrationDAte) : '';


    this.builderForm.Reset();
    this.builderForm.SetEditAdd(this.EditAdd().toString());


    this.builderForm.SetField({
      field: 'id',
      label: 'id',
      order: 1,
      required: false,
      type: 'text',
      validationRequired: false,
      enable: false,
      show: false,
      value: this.dataEquipment().id
    });
    this.builderForm.SetField({
      field: 'name',
      label: 'Equipment Name',
      order: 2,
      required: true,
      type: 'text',
      validationRequired: true,
      enable: true,
      show: true,
      value: this.dataEquipment().name
    });
    this.builderForm.SetField({
      field: 'description',
      label: 'Equipment Description',
      order: 3,
      required: true,
      type: 'textArea',
      validationRequired: true,
      enable: true,
      show: true,
      value: this.dataEquipment().description
    });
    this.builderForm.SetField({
      field: 'calibrationDate',
      label: 'Calibration Date',
      order: 4,
      required: true,
      type: 'date',
      validationRequired: true,
      enable: true,
      show: true,
      value: dateYYYMMDDCalibration
    });

   /* if (this.dataEquipment().userName) {
      this.usersSelect = this.usersSelects.find(x => x.text === this.dataEquipment().userName) ?? this.usersSelect;
  }*/
   
   /* this.builderForm.SetField({
      field: 'userName',
      label: 'Asign User',
      order: 4,
      required: true,
      type: 'select',
      validationRequired: true,
      enable: true,
      show: true,
      value: this.usersSelect.id,
      options: this.usersSelects,

    });*/

    this.builderForm.SetFormGroup(
      this.fb.group({
        id: [0],
        name: '',
        calibrationDate: '',
    //    employeeAccount: '',
      //  userName: ''
      })
    );
    this.builderForm.SetSubmitFunction(
      () => {

        this.SubmitRequests();
      }
    );
    this.builderForm.SetTitle('Test Prueba');
    this.genericForm = this.builderForm.Generate();



  }

  //ADD OR EDIT
  SubmitRequests(): void {

    console.log('Se hizo Submit');
    console.log(this.dataEquipment());
    // if( this.dataEquipment().length <1 ){

    //  return;
    //}
    if (this.genericForm.customFromGroup && this.genericForm.customFromGroup.valid) {
      const formValues = this.genericForm.customFromGroup.value;
      /*const status =   this.statuses.find((val)=>{
        if(val.id==formValues.status){
         return val;
        }
        return undefined;
       })?.text??'';*/

      /*if (formValues.userName != '') {
        this.usersSelect = this.usersSelects.find(x => x.id = formValues.userName ?? '') ?? this.usersSelect;
      }*/

      this.dataEquipment.set({
        id: formValues.id || '',
        calibrationDate: formValues.calibrationDate,
        name: formValues.name,
        description:  formValues.description
      });
      this.submit.set(true);


    }





    console.log('Se hizo Submit');
    console.log(this.dataEquipment());

    if(this.EditAdd()=='Add'){

      this.service.createEquipment(this.dataEquipment()).subscribe({
      next:(response)=> {

        this._message.add({
        severity:'success', 
        summary:'Add!', 
        detail:`Equipmment ${response.message} Added`,
        life : 2000
        });

        setTimeout(() => {
        this.GetTable(this.selectedStatus);
        }, 1000); // Delay of 1 second
        
      },
      error: ()=>{
        
      },
      complete: ()=>{

        
      }
      })
      
    }else{
      
      this.service.updateEquipment(this.dataEquipment()).subscribe({
        next:(response)=> {

          
          this._message.add({
            severity:'success', 
            summary:'Edit!', 
            detail:`Equipmment ${response.message} Updated`,
            life : 2000
          });
          this.GetTable(this.selectedStatus);
          
        },
        error: ()=>{
          
        },
        complete: ()=>{

          
        }
      })
      

    }
    
    
    this.displayMaximizable = false;
    this.dataEquipments = signal<EquipmentDto[]>
      (
        []

      );
    console.log(this.genericForm.data);
  }

  getModal(item: EquipmentDto = {} as EquipmentDto) {

    this.submit.set(false);


    //Put Header in Modal
    if (item.id == 0 || item.id == undefined) {
      this.EditAdd.set('Add')
    } else {
      this.EditAdd.set('Edit')
    }

    //Use Selected Object
    if (this.EditAdd() == 'Edit') {
      this.dataEquipment.set(item);
      this.ConfigForm();
      this.displayMaximizable = true;
      let tests: EquipmentDto;
      this.service.getEquipmentByID(item.id).subscribe({
        next: (data) => {
          tests = data;
          this.dataEquipment = signal<EquipmentDto>
            (

              tests
            );
        },
        error: (error) => {
          console.error(error);
          this.ConfigForm();
        },
        complete: () => {
          this.ConfigForm();
        }
      });

      /* this.dataTests=signal([...tests, this.testsTemp()]);
 
 
       this.dataTests = signal<Test[]>
       (
         
         [tests]
       );
       this.dataTests = signal< this.service.getTestRequest*/
    } else {

      const dataEquipmentTemp: EquipmentDto = {
        id: 1,
        calibrationDate: '',
        name: '',
        description:''

      }


      this.dataEquipment.set(dataEquipmentTemp);
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

  formatDateToYYYYMMDD(date: Date): string {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
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
