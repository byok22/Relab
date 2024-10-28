import { CommonModule, DatePipe } from '@angular/common';
import { ChangeDetectionStrategy, Component, OnInit, signal } from '@angular/core';
import { User } from '../../shared/interfaces/UsersInterfaces/user.interface';
import { GenericPageTableMenuForm } from '../../common/interfaces/generic-page-table-menu-form';
import { UsersService } from '../services/users.service';
import { TableBuilderFactoryService } from '../../shared/components/generic-table/service/table-builder-factory-service.service';
import { FormBuilder } from '@angular/forms';
import { MessageService } from 'primeng/api';
//import { GenericMenuInterface } from '../../../shared/components/generic-menu-item/interfaces/generic-menu-item.interface';
import { GenericTableConfig } from '../../shared/components/generic-table/interfaces/generic-table-config';
import { GenericMenuInterface } from '../../shared/components/generic-menu-item/interfaces/generic-menu-item.interface';
import { GenericFormInterface } from '../../shared/components/generic-form/generic-form.interface';
import { GenericFormConcretBuilder } from '../../shared/components/generic-form/builder/generic-form-concret-builder';
import { SelectOption } from '../../shared/interfaces/select-option.interface';
import { GenericStatus } from '../../shared/enums/generic-status.enum';
import { GenericMenuConcreteBuilder } from '../../shared/components/generic-menu/builder/generic-menu-concret-builder';
import { BasicKpi } from '../../shared/interfaces/basic-kpi.interface';
import { TableColumn } from '../../shared/components/generic-table/interfaces/table-column';
import { GenericMenuComponent } from '../../shared/components/generic-menu/generic-menu.component';
import { GenericTableComponent } from '../../shared/components/generic-table/generic-table.component';
import { HttpClientModule } from '@angular/common/http';
import { GenericTitleComponent } from '../../shared/components/generic-title/generic-title.component';
import { GenericFormComponent } from '../../shared/components/generic-form/generic-form.component';
import { PrimengModule } from '../../shared/modules/primeng.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { DropdownsService } from '../../common/service/dropdowns.service';
import { EmployeeType } from '../../shared/enums/employee-type.enum';

@Component({
  selector: 'app-users-page',
  standalone: true,
  imports: [
    CommonModule, GenericMenuComponent, GenericTableComponent, HttpClientModule, GenericTitleComponent, GenericFormComponent, PrimengModule, FontAwesomeModule
  ],
  providers: [DatePipe, DropdownsService, MessageService],
  templateUrl: './users-page.component.html',
  styleUrl: './users-page.component.css',
  changeDetection: ChangeDetectionStrategy.Default,
})
export class UsersPageComponent implements OnInit, GenericPageTableMenuForm<User> {

  /**
   *
   */
  constructor(    
    private userService: UsersService,
    private serviceTable: TableBuilderFactoryService,
    private datePipe: DatePipe,
    private fb: FormBuilder,
    private _message: MessageService 
  ) {
    this.FillMenu();
    this.ConfigMenu();
  }
  ngOnInit(): void {

   
    this.ConfigForm();
  }

  //#endregion Inits





  //#region  Variables
  //Builders
  builderTable = this.serviceTable.createBuilder<User>();
  //Menu
  menuItems: GenericMenuInterface[] = [];

  //Table
  tableConfig!: GenericTableConfig<User>;
  dataTable: User[] = [];
  hideTable = signal(true);
  public newTable = signal(true);

  public dataForm = signal<User>
    (
      {
        id: 1,
        employeeAccount:'',
        userName:'',
        email:'',
       
      }
    );

  public dataFormTemp = signal<User>
    (
      {
        id: 1,
        employeeAccount:'',
        userName:'',
        email:'',
      }
    );

  public EditAdd = signal<string>('');
  public displayMaximizable: boolean = false;


  //Form

  genericForm: GenericFormInterface<User> = {
    tittle: '',
    fields: [],
    customFromGroup: undefined,
    editAdd: '',
    data: this.dataForm(),
  }

 
  builderForm = new GenericFormConcretBuilder<User>();
  
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
    this.userService.getProfiles().subscribe({
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
    this.builderTable.SetTitle("Users Table");
    this.builderTable.SetDataKey("id");
    this.builderTable.SetData(this.dataTable);
    this.builderTable.SetKpis(this.GetKpis());
    this.builderTable.SetPagination(true);
    this.builderTable.SetRowsPerPage(10);
    this.builderTable.SetRowsPerPageOptions([5, 10, 20]);
    this.builderTable.SetColumns(this.getColumns());
    this.builderTable.SetGlobalFilterFields(["employeeAccount", "userName"]);
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
      { field: 'id', header: 'ID' },/*
      { field: 'name', header: 'Equipment' },
      { field: 'calibrationDate', header: 'Calibration Date' },*/
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

    const statusText = this.statusDD.find(x=>x.id == status);

    const employeeType: EmployeeType = EmployeeType[statusText as unknown as keyof typeof EmployeeType];

    try {
      this.userService.getUsersCompleteByType(employeeType??EmployeeType.All).subscribe({
        next: (userRequest) => {
          if(userRequest.length ==0){
            
            this._message.add({
              severity:'error', 
              summary:'Nodata!', 
              detail:`Users Not Found`,
              life : 2000
            });         
            return;
            

          }


          const transformeduserRequest = userRequest.map(request => ({
            ...request            
          }));

          this.dataTable = transformeduserRequest;

          

          if (this.newTable()) {
            this.ConfigTable();
            this.newTable.set(false);
            this.hideTable.set(false);
            return;
          }
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

  async ConfigForm() {         

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
      value: this.dataForm().id
    });
    this.builderForm.SetField({
      field: 'userName',
      label: 'User Name',
      order: 2,
      required: true,
      type: 'text',
      validationRequired: true,
      enable: true,
      show: true,
      value: this.dataForm().userName
    });
    this.builderForm.SetField({
      field: 'email',
      label: 'Email',
      order: 3,
      required: true,
      type: 'text',
      validationRequired: true,
      enable: true,
      show: true,
      value: this.dataForm().email
    });
    this.builderForm.SetField({
      field: 'employeeAccount',
      label: 'Employee Account',
      order: 4,
      required: true,
      type: 'text',
      validationRequired: true,
      enable: true,
      show: true,
      value: this.dataForm().employeeAccount,
      onInputChange: this.employeeAccountInputChange.bind(this) // Vincular la función de submit
    });

    this.builderForm.SetFormGroup(
      this.fb.group({
        id: [0],
        userName: '',
        email:'',
        employeeAccount:''   
      })
    );
    this.builderForm.SetSubmitFunction(
      () => {

        this.SubmitRequests();
      }
    );
    this.builderForm.SetTitle('Users');
    this.genericForm = this.builderForm.Generate();

  }

  

  //ADD OR EDIT
  SubmitRequests(): void {

    console.log('Se hizo Submit');
    console.log(this.dataForm());
   
    if (this.genericForm.customFromGroup && this.genericForm.customFromGroup.valid) {
      const formValues = this.genericForm.customFromGroup.value;     

      this.dataForm.set({
        id: formValues.id || '',
        userName: formValues.userName,
        email: formValues.email,
        employeeAccount: formValues.employeeAccount,     
      });
      this.submit.set(true);


    }

    console.log('Se hizo Submit');
    console.log(this.dataForm());

    if(this.EditAdd()=='Add'){

      this.userService.createUser(this.dataForm()).subscribe({
        next:(response)=> {

          
          this._message.add({
            severity:'success', 
            summary:'Add!', 
            detail:`User ${response.userName} Added`,
            life : 2000
          });
          
        },
        error: (err)=>{
          console.log(err);
          
        }
      })
      
    }else{
      this.userService.updateUser(this.dataForm()).subscribe({
        next:(response)=> {

          
          this._message.add({
            severity:'success', 
            summary:'Edit!', 
            detail:`User ${response.userName} Updated`,
            life : 2000
          });
          
        },
        error: ()=>{
          
        }
      })
      

    }

    
    this.displayMaximizable = false;    
    console.log(this.genericForm.data);
  }

  getModal(item: User = {} as User) {

    this.submit.set(false);


    //Put Header in Modal
    if (item.id == 0 || item.id == undefined) {
      this.EditAdd.set('Add')
    } else {
      this.EditAdd.set('Edit')
    }

    //Use Selected Object
    if (this.EditAdd() == 'Edit') {
      this.dataForm.set(item);
      this.ConfigForm();
      this.displayMaximizable = true;
      let tests: User;
      this.userService.getUserByID(item.id??0).subscribe({
        next: (data) => {
          tests = data;
          this.dataForm = signal<User>
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

      const dataUserTemp: User = {
        employeeAccount:'',
        id: 0,
        userName:'',
        email:''

      }


      this.dataForm.set(dataUserTemp);
      this.ConfigForm();
      this.displayMaximizable = true;
    }


  }

  employeeAccountInputChange($event:string):void{
    console.log('Esto es la pagina');
    console.log($event);
    console.log('Esto es la y el fin');
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
