import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, OnInit, signal } from '@angular/core';
import { GenericMenuComponent } from '../../shared/components/generic-menu/generic-menu.component';
import { GenericMenuInterface } from '../../shared/components/generic-menu/interfaces/generic-menu-item.interface';
import { GenericMenuConcreteBuilder } from '../../shared/components/generic-menu/builder/generic-menu-concret-builder';
import { SelectOption } from '../../shared/interfaces/select-option.interface';
import { GenericTableComponent } from '../../shared/components/generic-table/generic-table.component';
import { GenericTableConfig } from '../../shared/components/generic-table/interfaces/generic-table-config';
import { TableBuilderFactoryService } from '../../shared/components/generic-table/service/table-builder-factory-service.service';
import { BasicKpi } from '../../shared/interfaces/basic-kpi.interface';
import { TableColumn } from '../../shared/components/generic-table/interfaces/table-column';
import { TestRequestService } from '../services/test-request.service';
import { HttpClientModule } from '@angular/common/http';
import { TestRequestDto } from '../../shared/interfaces/test-request-interfaces/test-request-dto.interface';
import { DatePipe } from '@angular/common';
import { GenericTitleComponent } from '../../shared/components/generic-title/generic-title.component';
import { GenericFormConcretBuilder } from '../../shared/components/generic-form/builder/generic-form-concret-builder';
import { GenericFormComponent } from '../../shared/components/generic-form/generic-form.component';
import { FormBuilder } from '@angular/forms';
import { GenericFormInterface } from '../../shared/components/generic-form/generic-form.interface';
import { PrimengModule } from '../../shared/modules/primeng.module';
import { Test } from '../../shared/interfaces/test-interfaces/test.interface';
import { faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { DropdownsService } from '../../common/service/dropdowns.service';

import { TestStatusEnum } from '../../test-catalog/interface/test-status.enum';
import { TestFormCompleteComponent } from "../../test-catalog/components/test-form-complete/test-form-complete.component";
import { TestRequestEnum } from '../Enums/test-request.enum';
import { ChangeStatusTestRequest } from '../../test-catalog/interface/change-status-test-request';
import { ChangeStatusFormComponent } from "../../test-catalog/components/change-status-form/change-status-form.component";
import { MessageService } from 'primeng/api';

@Component({
  selector: 'test-requests',
  standalone: true,
  imports: [
    CommonModule, GenericMenuComponent, GenericTableComponent, HttpClientModule, GenericTitleComponent, GenericFormComponent, PrimengModule, FontAwesomeModule,
    TestFormCompleteComponent,
    ChangeStatusFormComponent
],
  providers:[TestRequestService, MessageService, DatePipe, DropdownsService],
  templateUrl: './test-requests.page.html',
  styleUrl: './test-requests.page.scss',
  changeDetection: ChangeDetectionStrategy.Default,
})
export class TestRequestsComponent implements OnInit {





  /**
   *
   */
  constructor(
    private serviceTable: TableBuilderFactoryService,
    private service: TestRequestService,
    private datePipe: DatePipe,
    private fb: FormBuilder,
    private serviceDD: DropdownsService,
    private _message: MessageService 
  ) {    
    
  }
  ngOnInit(): void {
    this.ConfigMenu();  
    this.FillMenu();
    this.ConfigForm(); 
  } 
  //Builders
  builderTable = this.serviceTable.createBuilder<TestRequestDto>();

  statusTemp: TestRequestEnum = TestRequestEnum.New;
  statusesTestRequest: TestRequestEnum[] = Object.values(TestRequestEnum);
  idTemp: number = 0;

  displayStatus: boolean = false;

  //Reviso si se hace un submit
  public submit = signal(false); 
  public submitTestForm = signal(false); 
  public newTable = signal(true);
  public EditAdd = signal<string>('Add');
  public saveUpdate:boolean = this.EditAdd()=='Add'?false:true;
  public displayMaximizable: boolean = false;  
  public displayTestModal: boolean = false;

  public faTrashAlt = faTrashAlt;

  //Menu
  menuItems: GenericMenuInterface[] = []; 
  

  //Table
  tableConfig!: GenericTableConfig<TestRequestDto>;
  dataTable:TestRequestDto[]=[];
  hideTable = signal(true);

  public dataTestRequest =signal<TestRequestDto>
  (
    {
      id: 1,
      status: TestRequestEnum.New,
      active: true,
      createdBy:'',
      createdAt: (new Date()).toDateString(),
      start:(new Date()).toDateString(),
      tentativeEnd:(new Date()).toDateString(),
      end:(new Date()).toDateString(),
      lastUpdatedBy: '',
      lastUpdatedMessage: '',
      user:'',
      description:''

    }   
  ) ;

  public testsTemp =signal<Test>
  (
    {
      id: 1,
      name:'',
      description:'',    
      status: TestStatusEnum.New 
    }   
  ) ;

  public dataTests = signal<Test[]>
  (
    []

  );







  //#region  Menu

  statusDD: SelectOption[]=[];
  selectedStatus: string ='';

  statusItem:GenericMenuInterface = {
    item: { 
      selectedOption: this.selectedStatus,
      options: this.statusDD, onChange: (event:string) => { 
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
    this.serviceDD.getStatus().subscribe({
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
      item: { onClick: () => {   

           this.GetTable(this.selectedStatus) ;
          
      
      } },
      labelText: 'Find',
      order: 5,
      type: 'button'
    });

    this.menuItems=builder.Generate();
  }
  //#endregion




  

  //#region Table 
  
   
  async ConfigTable():Promise<void>{   
    this.builderTable.Reset();
    this.builderTable.SetTitle("Custom Table");
    this.builderTable.SetDataKey("customId");
    this.builderTable.SetData(this.dataTable);
    this.builderTable.SetKpis(this.GetKpis());
    this.builderTable.SetPagination(true);
    this.builderTable.SetRowsPerPage(10);
    this.builderTable.SetRowsPerPageOptions([5, 10, 20]);
    this.builderTable.SetColumns(await this.getColumns());
    this.builderTable.SetGlobalFilterFields(["active", "status","description"]);
    //builder.SetGroupBy("category");
   // builder.Generate();

    this.tableConfig =  this.builderTable.Generate();

  }

  getModal(item: TestRequestDto = {} as TestRequestDto) {
    
    this.submit.set(false) ;

   
    //Put Header in Modal
    if(!item || item.id==0|| item.id == undefined){
      this.EditAdd.set('Add')
    }else{
      this.EditAdd.set('Edit')
    }
 
    //Show Modal
   
    //Use Selected Object
    if(this.EditAdd()=='Edit')
    {
      this.saveUpdate=true;
      this.service.getTestRequestById(item.id).subscribe({
        next:(data)=>{
          this.dataTestRequest.set(data);
          this.dataTests.set(data.tests??[]);
          this.ConfigForm();
          this.displayMaximizable = true;
        },
        error:(error)=>{
          console.error(error);
          this.ConfigForm();
        },
        complete:()=>{
          this.ConfigForm();
        }
      });
     /* this.dataTestRequest.set(item);
      let tests: Test[] = []
      this.dataTests.set(this.dataTestRequest().tests??tests)
      this.ConfigForm();
      this.displayMaximizable = true;*/
     
      /*this.service.getTestsFromRequest(item.id).subscribe({
        next:(data)=>{
          tests = data;
          this.dataTests = signal<Test[]>
          (
            
            tests
          );
        },
        error:(error)=>{
          console.error(error);
          this.ConfigForm();
        },
        complete:()=>{
          this.ConfigForm();
        }
      });*/

     /* this.dataTests=signal([...tests, this.testsTemp()]);


      this.dataTests = signal<Test[]>
      (
        
        [tests]
      );
      this.dataTests = signal< this.service.getTestRequest*/
    }else{

      this.saveUpdate=false;
      const dataTestTemp: TestRequestDto = {
        id: 0,
        status: TestRequestEnum.New,
        active: false,
        createdAt:(new Date()).toDateString(),
        start:(new Date()).toDateString(),
        tentativeEnd:(new Date()).toDateString(),
        end:(new Date()).toDateString(),
        lastUpdatedBy: '',
        lastUpdatedMessage: '',
        user:'',
        description:''
      }   
     

      this.dataTestRequest.set(dataTestTemp);
      this.ConfigForm();
      this.displayMaximizable = true;
    }
   
  }

  GetTable(status: string): void {
      

        try {
          const statusNumber = Number(status);
          const statusS = this.statuses[statusNumber-1];
         
          this.service.getTestRequest(statusS?statusS.text:'All').subscribe({
            next:(dataTestRequest)=>{
    
              dataTestRequest.forEach(request => {
                request.createdAt = this.datePipe.transform(request.createdAt, 'dd-MM-yyyy HH:mm:ss') ?? '';
                request.start = this.datePipe.transform(request.start, 'dd-MM-yyyy HH:mm:ss') ?? '';
                request.tentativeEnd = this.datePipe.transform(request.tentativeEnd, 'dd-MM-yyyy HH:mm:ss') ?? '';
                request.end = this.datePipe.transform(request.end, 'dd-MM-yyyy HH:mm:ss') ?? '';
                request.lastupdated = this.datePipe.transform(request.lastupdated, 'dd-MM-yyyy HH:mm:ss') ?? '';
                }); 
    
                this.dataTable= dataTestRequest;
    
                if(this.newTable())
                {
                  
                  this.ConfigTable( ); 
                  this.newTable.set(false);
                  this.hideTable.set(false);
                  return;
                }
                this.builderTable.SetData(this.dataTable);
                this.hideTable.set(false);
            }
    
            ,
            error:(error)=>{
              console.error(error);
    
            },
            complete:()=>{
    
            }
          })
              
        } catch (error) {
          console.error('Error fetching data', error);
          throw error;
        }      
   
  }

  GetKpis(): BasicKpi[] {
    return   [
      { title :"Total",  total:this.dataTable.length.toString()},      
    ];
  }
  //Function to get all columns and hide some fields
  async getColumns(): Promise<TableColumn[]> {
    // Definir columnas manualmente
    const manualColumns: TableColumn[] = [
      { field: 'id', header: 'ID' },
      { field: 'status', header: 'Status', genericButton:'Change Status' },
      { field: 'description', header: 'Description' },
      { field: 'createdBy', header: 'Created By' },
     // { field: 'user', header: 'User' },
     {field:'start', header:'Start'},
      {field:'end', header:'End'},
      
      { field: 'testsCount', header: 'Test Count' },      
      { field: 'createdAt', header: 'Created' },     
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
    const fieldsToHide = ["tests","active","tentativeEnd","user", "lastUpdatedBy", "lastupdated", "lastUpdatedMessage"];
    
    // Agregar propiedad showHeader para ocultar columnas
    columns = columns.map(column => ({
      ...column,
      showHeader: !fieldsToHide.includes(column.field)
    }));
  
    return columns;
  }

  changeStatus($event: TestRequestDto) {
    console.log($event);
    this.statusTemp = $event.status??TestRequestEnum.New
    this.idTemp = $event.id;

    this.displayStatus = true;

   }

   onStatusChanged($event: ChangeStatusTestRequest) {

    this.service.addStatusChangeToTestRequest(this.idTemp, $event);
    console.log($event);
    setTimeout(() => {
      this.displayStatus = false;
    }, 2000);
    
   }
  


capitalizeFirstLetter(word: string): string {
  if (!word) return word;
  return word[0].toUpperCase() + word.substr(1).toLowerCase();
}
  //#endregion
  






  //#region  Form TestRequests

  genericForm:GenericFormInterface<TestRequestDto>={
    tittle: '',
    fields: [],
    customFromGroup: undefined,
    editAdd: '',
    data:this.dataTestRequest()
  };

  public dataForm = signal<Test>
  (
    {
    id: 1,   
    name:'',
    description:''  ,
    status: TestStatusEnum.New 
    }
  );




  builderForm = new GenericFormConcretBuilder<TestRequestDto>();
 
  statuses: SelectOption[] = this.getEnumSelectOptions(TestRequestEnum);


  ConfigForm() {

    //Selects
    

    const selectValue = this.statuses.find((val)=>{
     if(val.text.toLowerCase()==this.dataTestRequest().status.toLowerCase()){
      return val;
     }
     return undefined;
    });
  
   
    this.builderForm.Reset();
    this.builderForm.SetEditAdd(this.EditAdd().toString());
    
    this.builderForm.SetField({
      field:'id',
      label:'id',
      order:1,
      required:false,
      type:'text',
      validationRequired:false,
      enable:false,
      show:false,
      value:this.dataTestRequest().id

    });
    this.builderForm.SetField({
      field:'status',
      label:'Status',
      order:2,
      required:true,
      type:'select',
      validationRequired:true,
      enable:this.EditAdd()=='Edit'?true:false,
      
      show:this.EditAdd()=='Edit'?true:false,
      value: selectValue?.id??'New',
     // value:this.dataTestRequest().status.toString(),
      options: this.statuses,

    });

    this.builderForm.SetField({
      field:'description',
      label:'Description',
      order:3,
      required:true,
      type:'text',
      validationRequired:true,
      enable:true,
      
      show:true,
      value:  this.dataTestRequest().description??'',
     // value:this.dataTestRequest().status.toString(),
     // options: this.statuses,

    });

   /* this.builderForm.SetField({
      field: 'customField',
      label: 'Custom Field',
      order: 3,
      required: false,
      type: 'custom',
      validationRequired: false,
      enable: true,
      show: true,
      value: '<div>{{customForm.get("customField")?.value}}</div>' // Tu plantilla HTML
    });
*/
    this.builderForm.SetField({
      field:'active',
      label:'Active',
      order:4,
      required:true,
      type:'checkbox',
      validationRequired:true,
      enable:true,
      show:true,
      value:this.dataTestRequest().active

    });
    this.builderForm.SetField({
      field:'createdBy',
      label:'Created By',
      order:5,
      required:true,
      type:'text',
      validationRequired:true,
      enable:false,
      show:false,
      value:this.dataTestRequest().createdBy??0

    });

    const startString: string = this.dataTestRequest().start;
    const fecha = this.parseDate(startString);

    const fechaYYYMMDD = this.formatDateToYYYYMMDD(fecha);

    const startStringEnd: string = this.dataTestRequest().end??'';
    const fechaEnd = this.parseDate(startStringEnd);

    const fechaYYYMMDDEnd = this.formatDateToYYYYMMDD(fechaEnd);

    
    this.builderForm.SetField({
      field:'start',
      label:'Start',
      order:5,
      required:true,
      type:'date',
      validationRequired:true,
      enable:true,
      show:true,
      value:fechaYYYMMDD

    });
    this.builderForm.SetField({
      field:'end',
      label:'End',
      order:6,
      required:true,
      type:'date',
      validationRequired:true,
      enable:true,
      show:true,
      value:fechaYYYMMDDEnd

    });
    this.builderForm.SetFormGroup(
      this.fb.group({
        id:[0], 
        status:['New'],
       // customField: [''],
        active:[true],
        createdBy:[0],
        start:[Date.now()]


        
      })
    );
    this.builderForm.SetSubmitFunction(
      ()=>{
        
        this.SubmitRequests();       
      }
    );
    this.builderForm.SetTitle('Test Prueba');
    this.genericForm = this.builderForm.Generate();
  }

  
   // Method to show the modal for adding tests
   showAddTestModal() {
    //this.ConfigTestForm();
    this.displayTestModal = true;
  }
  //#endregion





  //#region Tests Form



  
  removeTest(arg0: number) {
   this.dataTests.set(
      this.dataTests().filter(x=>x.id!=arg0)
   );
  }
  onTestFormSubmit($event: Test) {

    this.displayTestModal= false;
  
    this.dataTests().push($event);        
  
  }



  //#endregion





//#region Submits

SubmitRequests(): void{
  if( this.dataTests().length <1 ){

    return;
  }
  if (this.genericForm.customFromGroup && this.genericForm.customFromGroup.valid) {
    const formValues = this.genericForm.customFromGroup.value;
    const status =   this.statuses.find((val)=>{
      if(val.id==formValues.status){
       return val;
      }
      return undefined;
     })?.text??'';
     
     const statusEnumValue = TestRequestEnum[status as keyof typeof TestRequestEnum] ;

        
     this.dataTestRequest.set( {
       id: formValues.id || '',
       status: statusEnumValue?? TestRequestEnum.New,
       active: formValues.active || false,
       start: formValues.start,
       end: formValues.end,
       createdBy: formValues.createdBy ,   
       lastUpdatedBy: formValues.createdBy ,
       lastUpdatedMessage: 'New Register',
       createdAt: this.formatDateToYYYYMMDD(new Date()),
       user: formValues.user,
       description: formValues.description,
       tests: this.dataTests()
     });
    this.submit.set(true);   


  }

  if(this.EditAdd()=='Add'){
    this.service.create(this.dataTestRequest()).subscribe({
      next:(result)=>{

        if(result.isSuccessful){

          this._message.add({
            severity:'success', 
            summary:'Add!', 
            detail:`Test Request ${this.dataTestRequest().description} Added`,
            life : 2000
          });

        }else{

          
          this._message.add({
            severity:'warning', 
            summary:'Add!', 
            detail:`Error Test Request ${result.message}`,
            life : 2000
          });

        }

     

      }
    }
      
    );

  }else{


    this.service.update(this.dataTestRequest()).subscribe({
      next:(result)=>{

        if(result.isSuccessful){

          this._message.add({
            severity:'success', 
            summary:'Add!', 
            detail:`Test Request ${this.dataTestRequest().description} Updated`,
            life : 2000
          });

        }else{

          
          this._message.add({
            severity:'warning', 
            summary:'Add!', 
            detail:`Error Test Request ${result.message}`,
            life : 2000
          });

        }     
      }
    }
      
    );
  }
 
  console.log('Se hizo Submit');
  this.displayMaximizable = false;
  this.dataTests = signal<Test[]>
  (
    []

  );
  console.log(this.genericForm.data);
}



//#endregion Submits




  //#region Extras

  parseDate(dateString: string): Date|undefined {
    //if contains T convert to space
    if(dateString.includes('T')){
     return new Date(dateString);  
    }
    if(dateString==''|| dateString == undefined || dateString == null){
      return  undefined;
    }
    const [datePart, timePart] = dateString.split(' ');
    const [day, month, year] = datePart.split('-').map(Number);
    const [hours, minutes, seconds] = timePart.split(':').map(Number);
    return new Date(year, month - 1, day, hours, minutes, seconds);
  }

  formatDateToYYYYMMDD(date: Date|undefined): string {
    if(!date){
      return '';
    }
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
