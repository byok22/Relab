import { CommonModule, DatePipe } from '@angular/common';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit, signal } from '@angular/core';

import { BodyComponent } from '../../master-page/components/body/body.component';
import { GenericFormComponent } from '../../shared/components/generic-form/generic-form.component';
import { GenericMenuComponent } from '../../shared/components/generic-menu/generic-menu.component';
import { GenericTableComponent } from '../../shared/components/generic-table/generic-table.component';
import { GenericTitleComponent } from '../../shared/components/generic-title/generic-title.component';
import { SelectDropdownComponent } from '../../shared/components/select-dropdown/select-dropdown.component';
import { PrimengModule } from '../../shared/modules/primeng.module';
import { FullCalendarComponent } from '../../shared/components/full-calendar/full-calendar.component';
import { CalendarService } from '../services/calendar.service';
import { Test } from '../../shared/interfaces/test-interfaces/test.interface';
import { EventsFullCalendar, FullCalendarInterface } from '../../shared/components/full-calendar/fullcalendar.interface';
import { GenericFullCalendarConcretBuilder } from '../../shared/components/full-calendar/builder/generic-fullcalendar-concret-builder';
import { HttpClientModule } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { GenericFormInterface } from '../../shared/components/generic-form/generic-form.interface';
import { GenericFormConcretBuilder } from '../../shared/components/generic-form/builder/generic-form-concret-builder';
import { FormBuilder } from '@angular/forms';
import { GenericMenuInterface } from '../../shared/components/generic-menu/interfaces/generic-menu-item.interface';
import { SelectOption } from '../../shared/interfaces/select-option.interface';
import { GenericMenuConcreteBuilder } from '../../shared/components/generic-menu/builder/generic-menu-concret-builder';
import { DropdownsService } from '../../common/service/dropdowns.service';
import { TestDto } from '../../shared/interfaces/test-interfaces/testdto.interface';
import { response } from 'express';
import { GenericResponse } from '../../shared/interfaces/response/generic-response';
import { MessageService } from 'primeng/api';
import { TestStatusEnum } from '../../test-catalog/interface/test-status.enum';
import { GenericUpdateFormComponent } from '../../shared/components/generic-update-form/generic-update-form.component';
import { GenericUpdate } from '../../shared/interfaces/generic-update.interface';


@Component({
  selector: 'app-tests-calendar',
  standalone: true,
  imports: [
    CommonModule, GenericTableComponent, GenericMenuComponent, SelectDropdownComponent, PrimengModule, GenericFormComponent
    , BodyComponent, FullCalendarComponent, GenericTitleComponent, HttpClientModule
    ,GenericUpdateFormComponent
  ],
  providers:[CalendarService,DropdownsService, DatePipe, MessageService],
  templateUrl: './tests-calendar.component.html',
  styleUrl: './tests-calendar.component.css',
  changeDetection: ChangeDetectionStrategy.Default,
})
export class TestsCalendarComponent implements OnInit { 
 
  //#region Init
  /**
   *
   */

 

  constructor(
      private calendarService: CalendarService,     
      private fb: FormBuilder,
      private serviceDD: DropdownsService,  
      private datePipe: DatePipe,  
      private _message: MessageService 
    ) {
     
   
    
    
  }
  currentUpdate: GenericUpdate = {
    id: 0,
    updatedAt: new Date().toISOString(),
    message: ''
  };
  ngOnInit(): void {

    this.configCalendar();
    this.ConfigMenu();
    this.FillMenu();
    this.ConfigTestForm();
  
   
   

   /* this.calendarService.getAllTests().subscribe({
      next: (tests)=>{       
        this.tests = tests;
        
      },
      error:(err)=>{
        console.log(err);
      }

    }
    );*/
  }
  //#endregion Init

   //#region Variables
    //Menu
    menuItems: GenericMenuInterface[] = []; 

    tests: Test[] =[];
    tempTest: Test ={
      id:0,
      status: TestStatusEnum.New 

    };
     //FullCalendar
    genericFullCalendar: FullCalendarInterface<Test> = {
      events:[]
    };
    public displayMaximizable: boolean = false;
    public maximizeUpdate: boolean = false;
    builderTestForm = new GenericFormConcretBuilder<TestDto>();
    
    public testsTemp =signal<TestDto>
    (
      {
        id: 0,
        name: '',
        description: '',
        // profile:new Blob(),
        start: (new Date()).toDateString(),
        end: (new Date()).toDateString(),
        status: TestStatusEnum.New
      }   
    ) ;

    genericForm:GenericFormInterface<TestDto>={
      tittle: '',
      fields: [],
      customFromGroup: undefined,
      editAdd: '',
      data:this.testsTemp(),
    };
    public submitTestForm = signal(false); 
    //Response
    response: GenericResponse={
      message: '',
      pk: 0
    };
   // builder = new GenericMenuConcreteBuilder(this.menuItems);


    
   
   

  //#endregion





  //#region Menu

  statusDD: SelectOption[]=[];
  selectedStatus: string ='';
  statusItem:GenericMenuInterface = {
    item: { 
      selectedOption: this.selectedStatus,
      options: this.statusDD, onChange: (event:string) => { 
        this.selectedStatus = event;
        let status:string = this.statusDD.find(x => x.text === event)?.text || '';     
        this.UpdateCalendar(status);
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
    this.menuItems=builder.Generate();

  }


  
    
 
 
  //#endregion




  //#region  Calendar

  //Update Calendar Send Status
  UpdateCalendar(status: string) {
    this.calendarService.getTestsByStatus(this.selectedStatus).subscribe({
      next: (testsc: Test[]) => {
      
        
        

        this.tests = testsc;
        console.log(this.tests);
        this.configCalendar(1);
      }
    });
  }
  async configCalendar(regenerate:number =0): Promise<void>{
    const builder = new GenericFullCalendarConcretBuilder<Test>();
    builder.Reset();
    if(regenerate==0)
      this.tests = await firstValueFrom(this.calendarService.getTestsByStatus(this.selectedStatus));
    this.tests.forEach((test)=>{

      builder.SetEvent(
        {
          title:test.name??'',
          start: test.start??new Date(),
          end:  test.end??new Date(),
          description: test.description??'no hay desc',
          color: this.getRandomColors(), // Rojo,
          data:test
        },
      );


    })
    
    this.genericFullCalendar = builder.Generate();
  //  this.cd.markForCheck(); // Marca el componente para la detección de cambios
  }
  onEventSelected(event: EventsFullCalendar<Test>) {
    if (event.data) {
      this.calendarService.getTestByID(event.data.id).subscribe({
        next: (test) => {
          this.tempTest = test;

          const testTransform: TestDto = {
            ...test,
            start: this.datePipe.transform(test.start, 'dd-MM-yyyy HH:mm:ss') ?? '',
            end: this.datePipe.transform(test.end, 'dd-MM-yyyy HH:mm:ss') ?? '',
          };

          this.displayMaximizable = true;
          console.log('Evento seleccionado:', test);
          this.testsTemp.set(testTransform);
          this.ConfigTestForm();
        },
        error: (err) => {
          console.error('Error fetching test by id:', err);
        }
      });
    }
  }
  
  //#endregion




  //#region  Form
  ConfigTestForm() {
    //Change String Dates to TypescriptDate 
    const startString: string = this.testsTemp().start??'';
    const fecha = this.parseDate(startString);

    const fechaYYYMMDD = this.formatDateToYYYYMMDD(fecha);

    const endString: string = this.testsTemp().end??'';
    const fechaEnd = this.parseDate(endString);

    const fechaYYYMMDDEnd = this.formatDateToYYYYMMDD(fechaEnd);
    this.builderTestForm.Reset();
    this.builderTestForm.SetEditAdd('Add');

    this.builderTestForm.SetCustomNameButton('Update');

    this.builderTestForm.SetField({
      field:'id',
      label:'id',
      order:1,
      required:false,
      type:'number',
      validationRequired:false,
      enable:false,
      show:false,
      value:this.testsTemp().id

    });
    
    this.builderTestForm.SetField({
      field: 'name',
      label: 'Test Name',
      order: 2,
      required: true,
      type: 'text',
      validationRequired: true,
      enable: true,
      show: true,
      value: this.testsTemp().name
    });

    this.builderTestForm.SetField({
      field: 'description',
      label: 'Description',
      order: 3,
      required: false,
      type: 'text',
      validationRequired: false,
      enable: true,
      show: true,
      value: this.testsTemp().description
    });

    this.builderTestForm.SetField({
      field: 'status',
      label: 'Status',
      order: 4,
      required: false,
      type: 'select',
      validationRequired: false,
      enable: true,
      show: true,
      value: this.testsTemp().status??TestStatusEnum.New,
      options: this.statusDD
    });


   

    this.builderTestForm.SetField({
      field: 'start',
      label: 'Start',
      order: 5,
      required: false,
      type: 'date',
      validationRequired: false,
      enable: true,
      show: true,
      value:   fechaYYYMMDD,
    });
    this.builderTestForm.SetField({
      field: 'end',
      label: 'End',
      order: 6,
      required: false,
      type: 'date',
      validationRequired: false,
      enable: true,
      show: true,
      value:  fechaYYYMMDDEnd ,
    });

    this.builderTestForm.SetFormGroup(
      this.fb.group({
        id: [0],
        name: [''],
        description: [''],
        start:[''],
        end:[''],
      })
    );

    this.builderTestForm.SetSubmitFunction(() => {
      this.SubmitTest();     
    });

    this.builderTestForm.SetTitle('Test');
    this.genericForm = this.builderTestForm.Generate();
  }
  SubmitTest(){

    if (this.genericForm.customFromGroup && this.genericForm.customFromGroup.valid) {
      this.updateMessage();
    
      this.maximizeUpdate = true;
    }

    
    

    
   
  
  }

  updateMessage() {   
    this.maximizeUpdate = false;
    
      const formValues = this.genericForm.customFromGroup?.value;  
      
      this.calendarService.updateTestDates(formValues.id, formValues.start, formValues.end)
        .subscribe({
          next:(response)=>{
            this.response = response;
            if(this.response.message='Updated successfully'){
              this._message.add({
                severity:'success', 
                summary:'Updated!', 
                detail:'',
                life : 1000
              });
            }
            let status:string = this.statusDD.find(x => x.text === this.selectedStatus)?.text || '';  
            this.UpdateCalendar(status);

          }
        });
       this.testsTemp.set( {
         id: formValues.id || '',
         name: formValues.name,
         description: formValues.description,
         start: formValues.start,
         end: formValues.end,
         status: TestStatusEnum.New
       });
      this.submitTestForm.set(true);    
      //this.dataTests.mutate(tests => {tests.push(this.testsTemp())}
    
  
  
    console.log('Prueba agregada');
    this.displayMaximizable = false;
    this.testsTemp.set( {
      id: 0,
      name: '',
      description: '',
      status: TestStatusEnum.New
    });
  
   
   

  }
  
    
  
  //#endregion


  //#region  Extras
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
  getRandomColors():string{
    const colors: string[] =[
      '#2D97FA',
      '#462DFA',
      '#2D56FA',
      '#2DD7FA',
      '#892DFA',
      '#8099FA',
      '#1B57FA',
      '#6F1BFA',
    ];

    const randomNumber: number = this.getRandomInt(0,7)??0;


    return colors[randomNumber];



  }
  getRandomInt(min: number, max: number): number {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }
  //#endregion
}
