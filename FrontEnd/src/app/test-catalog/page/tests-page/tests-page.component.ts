import { CommonModule, DatePipe } from '@angular/common';
import { ChangeDetectionStrategy, Component, OnInit, signal, WritableSignal } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { GenericFormComponent } from '../../../shared/components/generic-form/generic-form.component';
import { GenericMenuComponent } from '../../../shared/components/generic-menu/generic-menu.component';
import { GenericTableComponent } from '../../../shared/components/generic-table/generic-table.component';
import { GenericTitleComponent } from '../../../shared/components/generic-title/generic-title.component';
import { PrimengModule } from '../../../shared/modules/primeng.module';
import { MessageService } from 'primeng/api';
import { Test } from '../../../shared/interfaces/test-interfaces/test.interface';
import { GenericPageTableMenuForm } from '../../../common/interfaces/generic-page-table-menu-form';
import { TableColumn } from '../../../shared/components/generic-table/interfaces/table-column';
import { TableBuilderFactoryService } from '../../../shared/components/generic-table/service/table-builder-factory-service.service';
import { GenericMenuInterface } from '../../../shared/components/generic-menu-item/interfaces/generic-menu-item.interface';
import { GenericTableConfig } from '../../../shared/components/generic-table/interfaces/generic-table-config';
import { GenericFormConcretBuilder } from '../../../shared/components/generic-form/builder/generic-form-concret-builder';
import { GenericFormInterface } from '../../../shared/components/generic-form/generic-form.interface';
import { BasicKpi } from '../../../shared/interfaces/basic-kpi.interface';
import { SelectOption } from '../../../shared/interfaces/select-option.interface';
import { TestsService } from '../../services/tests.service';
import { GenericMenuConcreteBuilder } from '../../../shared/components/generic-menu/builder/generic-menu-concret-builder';
import { FormBuilder } from '@angular/forms';
import { TestTable } from '../../interface/test-table.interface';

import { TestFormCompleteComponent } from "../../components/test-form-complete/test-form-complete.component";
import { TestStatusEnum } from '../../interface/test-status.enum';
import { ChangeStatusFormComponent } from '../../components/change-status-form/change-status-form.component';
import { ChangeStatus } from '../../interface/change-status.';
import { GenericResponse } from '../../../shared/interfaces/response/generic-response';


@Component({
  selector: 'app-tests-page',
  standalone: true,
  imports: [
    CommonModule, GenericMenuComponent, GenericTableComponent, GenericTitleComponent, GenericFormComponent, PrimengModule, FontAwesomeModule,
  
    TestFormCompleteComponent, ChangeStatusFormComponent
],
  providers: [DatePipe, MessageService],
  templateUrl: './tests-page.component.html',
  styleUrl: './tests-page.component.css',
  changeDetection: ChangeDetectionStrategy.Default,
})
export class TestsPageComponent implements OnInit, GenericPageTableMenuForm<Test> {


  statusTemp: TestStatusEnum = TestStatusEnum.New;
  statuses: TestStatusEnum[] = Object.values(TestStatusEnum);
  idTemp: number = 0;


  

  
  displayStatus: boolean = false;

  

  /**
   *
   */
  constructor(
    private serviceTable: TableBuilderFactoryService,
    private testService: TestsService,
    private fb: FormBuilder,
    private _message: MessageService 
  ) {
    
    this.FillMenu();
    this.ConfigMenu();
  }
  ConfigForm(): void {
    throw new Error('Method not implemented.');
  }
  
  ngOnInit(): void {
   // this.ConfigForm();
  }
  GetKpis(): BasicKpi[] {
    return [
      { title :"Total",  total:this.dataTable.length.toString()},   
    ]
    //throw new Error('Method not implemented.');
  }
  
    

  //#region  Variables

  //Menu
  menuItems: GenericMenuInterface[] = [];

  tableConfig!: GenericTableConfig<TestTable>;
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


  //Table 

  builderTable = this.serviceTable.createBuilder<TestTable>();

  dataTable: TestTable[] = [];     

  public newTable = signal(true);
  
  public hideTable = signal(true);


  //Form

  public dataForm =signal<Test> 
  (
    {
    id: 1,   
    name:'',
    description:''  ,
    status: TestStatusEnum.New 
    }
  );

  dataFormTemp: WritableSignal<Test> = signal<Test>
  (
    {
      id: 1,   
      name:''  ,
      description:''   ,
      status: TestStatusEnum.New    
    }
  );
  EditAdd: WritableSignal<string>= signal('');
  displayMaximizable: boolean = false;

  genericForm: GenericFormInterface<Test> = 
  { tittle: '', fields: [], customFromGroup: undefined, editAdd: '', data: this.dataForm()};

  builderForm: GenericFormConcretBuilder<Test> = new GenericFormConcretBuilder<Test>();

  submit: WritableSignal<boolean> = signal(false);

  


  //#endregion



  //#region  Menu

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
      labelText: 'Search',
      order: 2,
      type: 'button'
    });

    this.menuItems = builder.Generate();
  }

  
  FillMenu(): void {    
    this.testService.getStatus().subscribe({
      next: (status) => {
        this.statusDD = status;
        this.selectedStatus = status[0]?.id || '1'; // Asegúrate de que selectedBuilding tiene un valor válido
        this.statusItem.item.options = this.statusDD;
        this.statusItem.item.selectedOption = this.selectedStatus;
      }
    });
        
  }
  

  //#endregion


  //#region   Table

  ConfigTable(): void {
    this.builderTable.Reset();
    this.builderTable.SetTitle("Tests Table");
    this.builderTable.SetDataKey("id");
    this.builderTable.SetData(this.dataTable);
    this.builderTable.SetKpis(this.GetKpis());
    this.builderTable.SetPagination(true);
    this.builderTable.SetRowsPerPage(10);
    this.builderTable.SetRowsPerPageOptions([5, 10, 20]);
    this.builderTable.SetColumns(this.getColumns());
    this.builderTable.SetGlobalFilterFields(["id", "name"]);
    this.tableConfig = this.builderTable.Generate();
  }
  
  getColumns(): TableColumn[] {
    // Definir columnas manualmente
    const manualColumns: TableColumn[] = [
      { field: 'id', header: 'ID' },
      { field: 'status', header: 'Status', genericButton:'Change Status' },
      { field: 'name', header: 'Test' },
      { field: 'start', header: 'Start' },
      { field: 'end', header: 'End' },      
      { field: 'enginner', header: 'Enginner' },      
      { field: 'technicians', header: 'Technician' },    
      //{ field: 'LastUpdatedMessage', header: 'Last Updated Message' },   
      
      
      

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
    const fieldsToHide = ["description", "specialInstructions","samples"];

    // Agregar propiedad showHeader para ocultar columnas
    columns = columns.map(column => ({
      ...column,
      showHeader: !fieldsToHide.includes(column.field)
    }));

    return columns;
  }
  GetTable(...args: any[]): void {    
    const statusText = this.statusDD.find(x=>x.id == this.selectedStatus);

    try {
      this.testService.getTestsByStatus(statusText?.text??'').subscribe({
        next: (tests) => {
          if(tests.length ==0){
            
            this._message.add({
              severity:'error', 
              summary:'Nodata!', 
              detail:`Tests Not Found`,
              life : 2000
            });         
            return;
            

          }


          const transformeduserRequest = tests.map(test => ({
            ...test            
          }));

          this.dataTable = transformeduserRequest;

          

          if (this.newTable()) {
            this.ConfigTable();
            this.newTable.set(false);
            this.hideTable.set(false);
            return;
          }else{
            this.ConfigTable();
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

  changeStatus($event: TestTable) {
     console.log($event);
     this.statusTemp = $event.status??TestStatusEnum.New
     this.idTemp = $event.id;

     this.displayStatus = true;

    }

    onStatusChanged($event: ChangeStatus) {
      $event.idTest = this.idTemp;

      this.testService.addStatusChangeToTest($event).subscribe({
        next: (response) => {
          console.log(response);
          this._message.add({
            severity:'success', 
            summary:'Status Changed!', 
            detail:`Status Changed to ${response.message}`,
            life : 2000
          });
          this.displayStatus = false;
          this.hideTable.set(true);
          this.GetTable(this.selectedStatus);
        },
        error: (error) => {
          console.error(error);
          this._message.add({
            severity:'error', 
            summary:'Error!', 
            detail:`Error Changing Status`,
            life : 2000
          });
        }
      });

      
     }

  //#endregion


  //#region Form

  SubmitRequests(): void {
        console.log('Se hizo Submit');
        console.log(this.dataForm());
      
        if (this.genericForm.customFromGroup && this.genericForm.customFromGroup.valid) {
          const formValues = this.genericForm.customFromGroup.value;     

          this.dataForm.set({
            id: formValues.id || '',
            name: formValues.name,
            description: formValues.description,
            profile: formValues.profile, 
            status: TestStatusEnum.New     
          });
          this.submit.set(true);


        }

        console.log('Se hizo Submit');
        console.log(this.dataForm());

        
    console.log(this.genericForm.data);
    }


  getModal(item: TestTable): void {
    this.displayMaximizable = true;
    this.submit.set(false);
     //Put Header in Modal
     if (item.id == 0 || item.id == undefined) {
      this.EditAdd.set('Add')
    } else {
      this.EditAdd.set('Edit')
    }
  
    
     //Use Selected Object
    if (this.EditAdd() == 'Edit') {
     // this.dataForm.set(this.dataFormTemp());
     // this.ConfigForm();
      this.displayMaximizable = true;
      let tests: Test;
      this.testService.getTestByID(item.id).subscribe({
        next: (data) => {
          tests = data;
          
          this.dataForm.set(tests);
            

              tests
            ;
        },
        error: (error) => {
          console.error(error);
       //   this.ConfigForm();
        },
        complete: () => {
        //  this.ConfigForm();
        }
      });

     
    } else {

      this.dataFormTemp()

      const dataTestTemp: Test = {       
        id: 0,
        name:"",
        description: ""  ,
        status: TestStatusEnum.New      
      }


      this.dataForm.set(dataTestTemp);
   //   this.ConfigForm();
      this.displayMaximizable = true;
    }
  }

  async onTestFormSubmit($event: Test) {

    if(this.EditAdd()=='Add'){
      try {
        const observableResponse = await this.testService.create($event);
        observableResponse.subscribe(
          response => {
            this._message.add({
              severity: 'success',
              summary: 'Add!',
              detail: `User ${response.message} Added`,
              life: 2000
            });
            this.displayStatus = false;
            this.hideTable.set(true);
            this.GetTable(this.selectedStatus);
          },
          err => {
            console.log(err);
          }
        );
      } catch (err) {
        console.log(err);
      }
        
      }else{
        (await this.testService.update($event)).subscribe({
          next: (response: GenericResponse) => {
            this._message.add({
              severity: 'success',
              summary: 'Edit!',
              detail: `User ${response.message} Updated`,
              life: 2000
            });
            this.displayStatus = false;
            this.hideTable.set(true);
            this.GetTable(this.selectedStatus);
          },
          error: (err: any) => {
            console.log(err);
          }
        })
        
    }
    this.displayMaximizable = false;    
  }
    




  //#endregion




  //#region  Form Status

  

  
  //#endregion

  //#region Extra

 
  capitalizeFirstLetter(word: string): string {
    if (!word) return word;
    return word[0].toUpperCase() + word.substr(1).toLowerCase();
  }
  //#endregion
  
 
 
  
}
