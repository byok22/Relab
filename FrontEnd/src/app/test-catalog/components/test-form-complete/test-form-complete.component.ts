import { Component, Input, Output, EventEmitter, OnInit, signal, OnChanges, SimpleChanges } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';  // Importar ReactiveFormsModule

import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Test } from '../../../shared/interfaces/test-interfaces/test.interface';
import { EquipmentService } from '../../../equipments/services/equipment.service';
import { CommonModule, DatePipe } from '@angular/common';
import { BasicGenericTableWithSelectComponent } from "../../../shared/components/basic-generic-table-with-select/basic-generic-table-with-select.component";
import { ColumnasFields } from '../../../shared/components/basic-generic-table-with-select/interfaces/columns-field-generic.interface';
import { PrimengModule } from '../../../shared/modules/primeng.module';
import { EmployeeType } from '../../../shared/enums/employee-type.enum';
import { UsersService } from '../../../users/services/users.service';
import { Specification } from '../../../shared/interfaces/test-interfaces/specification.interface';
import { GenericFormInterface } from '../../../shared/components/generic-form/generic-form.interface';
import { GenericFormConcretBuilder } from '../../../shared/components/generic-form/builder/generic-form-concret-builder';
import { GenericFormComponent } from '../../../shared/components/generic-form/generic-form.component';
import { Attachment } from '../../../shared/interfaces/test-interfaces/attachment.interface';
import { testFormCompleteInterface } from '../../interface/test-form-complete.interface';
import { Samples } from '../../../shared/interfaces/test-interfaces/samples.interface';
import { Equipment } from '../../../shared/interfaces/equipments/equipment.interface';
import { TestStatusEnum } from '../../interface/test-status.enum';
import { LocalStorageService } from '../../../shared/services/localStorage.service';
import { GenericUpdateFormComponent } from '../../../shared/components/generic-update-form/generic-update-form.component';
import { GenericUpdate } from '../../../shared/interfaces/generic-update.interface';
import { Employee } from '../../../shared/interfaces/employees/employee.interface';
import { TestsService } from '../../services/tests.service';
import { GenericResponse } from '../../../shared/interfaces/response/generic-response';
import { EmployeesService } from '../../../employees/services/employees.service';



@Component({
  selector: 'test-form-complete',
  templateUrl: './test-form-complete.component.html',
  styleUrls: ['./test-form-complete.component.scss'],
  standalone: true,
  
  imports: [
    ReactiveFormsModule, // Añadir ReactiveFormsModule aquí,
    BasicGenericTableWithSelectComponent,
    PrimengModule,
    CommonModule,
    GenericFormComponent,
    GenericUpdateFormComponent
],
})
export class TestFormCompleteComponent implements OnInit, OnChanges, testFormCompleteInterface {



  //#region  Init

  ngOnInit(): void {
    this.ConfigForm();
  }

  currentUpdate: GenericUpdate = {
    id: 0,
    updatedAt: new Date().toISOString(),
    message: ''
  };

  constructor(
    private localStorageService: LocalStorageService,
    private equipmentService: EquipmentService,
    private datePipe: DatePipe,
    private fb: FormBuilder,
    private usersService: EmployeesService,
    private testService: TestsService
  ){
    this.fillBaseForms();
    this.initReactiveForm();

    if (!this.testForm) {
      this.initReactiveForm();
      
    }

  }
 
  
  ngOnChanges(changes: SimpleChanges): void {
    console.log("hay un cambio");
   
    this.fillForm();
   
  }
  fillForm() {
    if(!this.testData)
      return;

    ///const startString: string = this.testData.start??'';
  //const fecha = this.parseDate(startString);

    const fechaYYYMMDD = this.formatDateToYYYYMMDD(this.testData.start??new Date());

    //const endString: string = this.testsTemp().end??'';
    //const fechaEnd = this.parseDate(endString);

    const fechaYYYMMDDEnd = this.formatDateToYYYYMMDD(this.testData.end??new Date());
    
    this.testForm = this.fb.group({
      id: [this.testData.id,{ value: null, disabled: true }],
      name: [this.testData.name, Validators.required],
      description: [this.testData.description, Validators.required],
      start: [fechaYYYMMDD,''],
      end: [fechaYYYMMDDEnd,''],
      idRequest: [this.testData.idRequest],
      enginner: [this.testData.enginner],
      profile: [this.testData.profile],
      specialInstructions:[this.testData.specialInstructions],
      selectedRow: [null],
      quantity:[this.testData.samples?.quantity],
      size:[this.testData.samples?.size],
      weight:[this.testData.samples?.weight],
      status: [this.testData.status]

    });
    if(this.testData.profile)
    {
      this.profileAttachment = this.testData.profile;
    }
    this.testForm.get('end')?.setValidators([this.endDateValidator.bind(this)]);
  }

  initReactiveForm(){
    this.testForm = this.fb.group({
      id: [{ value: null, disabled: true }],
      name: ['', Validators.required],
      description: ['', Validators.required],
      start: [''],
      end: [''],
      idRequest: [null],
      enginner: [null],
      profile: [null],
      specialInstructions:[null],
      selectedRow: [null],
      size:[0],
      quantity:[0],
      weight:[0],
      status: [null],      
    });
    this.testForm.get('end')?.setValidators([this.endDateValidator.bind(this)]);

  }
  endDateValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const startDate = this.testForm.get('start')?.value;
    const endDate = control.value;

    if (startDate && endDate && new Date(startDate) >= new Date(endDate)) {
      return { 'endDateBeforeStartDate': true };
    }
    return null;
  }


  fillBaseForms(){
    
    this.usersService.getEmployeesByType(EmployeeType.Engineer).subscribe({
      next: (employee) =>{
        this.engs = employee

      },
      error: (erorr) =>{

      },
      complete: () => { }
    });

    this.usersService.getEmployeesByType(EmployeeType.Technician).subscribe({
      next: (employee) =>{
        this.techs = employee

      },
      error: (erorr) =>{

      },
      complete: () => { }
    });
    this.equipmentService.getEquipmentsByStatus('Active').subscribe({
      next: (equipmentRequest) => {
        const transformedEquipmentRequest = equipmentRequest.map(request => ({
          ...request,
          calibrationDate: this.datePipe.transform(request.calibrationDate, 'dd-MM-yyyy HH:mm:ss') ?? ''
        }));
        this.equipments = transformedEquipmentRequest;

       
      },
      error: (error) => {
        console.error(error);
      },
      complete: () => { }
    });
  }
  //#endregion





  //#region  Profile
    
  onFileChange(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length) {
      const file = input.files[0];
      this.profileAttachment = {
        name: file.name,
        file: file,
      }; 
      this.profileAttachment.url = URL.createObjectURL(file);     
     
      this.testForm.patchValue({
        profile: this.profileAttachment
      });
    }
  }
  
    

  downloadProfile() {
    console.log("download");
    if (this.profileAttachment && this.profileAttachment.url) {
      const link = document.createElement('a');
      link.href = this.profileAttachment.url;
      link.download = 'profile'; // You can modify the file name
      link.click();
    }
  }
  deleteProfile() {
    if(this.testData.id>=0){
      this.testService.deleteProfile(this.testData.id).subscribe({
        next: (response) =>{
          if(response.isSuccessful){
          this.testData.profile = undefined;
          this.testForm.patchValue({
            profile: null
          });
          this.profileAttachment = undefined;
        }
        },
        error: (error) =>{
          console.error(error);
        },
        complete: () => { }
      });
      return;
    }else{
      this.testData.profile = undefined;
      this.testForm.patchValue({
        profile: null
      });
    }
  }


  //#endregion





  //#region  Vars

  profileAttachment?: Attachment;

  addEdit: string ='Add';


  displayMaximizableEquipments: boolean=false;
  displayMaximizableTech: boolean=false;
  displayMaximizableEng: boolean=false;
  displayMaximizableSpecifications: boolean = false;
  maximizeUpdate: boolean = false;

  @Output() formSubmit = new EventEmitter<Test>();
  @Output() formCancel = new EventEmitter<void>();
    


  
  @Input() 
  testForm!: FormGroup;

  @Input()
  testData!: Test;

  @Input()
  isBasic: Boolean = false;

  

  equipments?: {
        
        id: number;
        name: string;
        description: string;
        calibrationDate: string;
       
    }[];
  
  engs: Employee[]=[];

  techs: Employee[]=[];

  attachments: Attachment[] =[];

  specifications: Specification[] =[];

  columsField: ColumnasFields[] =[
    {
      columName: 'ID',
      columnField: 'id'
    },
    {
      columName: 'Equipment',
      columnField: 'name'
    },   
    {
      columName: 'Description',
      columnField: 'description'
    },
    {
      columName: 'Calibration Date',
      columnField: 'calibrationDate'
    },
    
  ];

  columsFieldSpecifications: ColumnasFields[] =[
    {
      columName: 'Specification Name',
      columnField: 'specificationName'
    },
    {
      columName: 'Detalis',
      columnField: 'details'
    },
    {
      columName: 'Calibration Date',
      columnField: 'calibrationDate'
    },
  ];
 

  columsFieldUsers: ColumnasFields[] =[
    {
      columName: 'ID',
      columnField: 'id'
    },
    {
      columName: 'Employee Number',
      columnField: 'employeeNumber'
    },
    {
      columName: 'Name',
      columnField: 'name'
    },
  ];

  

  //#endregion


 


  //#region  Attachments

  onAttachmentChange(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length) {
      const file = input.files[0];
      const attachment: Attachment = {
        name: file.name,
        file: file
      };
      if (this.testData.attachments) {
        if(!this.testData.attachments.find(x=>x.name==attachment.name)){
          this.testData.attachments.push(attachment);
        }
        
      } else {
        this.testData.attachments = [attachment];
      }
    }
  }

  downloadAttachment(attachment: Attachment) {
    const url = attachment.url ?? (attachment.file ? URL.createObjectURL(attachment.file) : '');
    const link = document.createElement('a');
    link.href = url;
    link.download = attachment.name;
    link.click();
    URL.revokeObjectURL(url);
  }

  deleteAttachment(index: number) {
      if(this.testData.id>=0){
        const attachmentId = this.testData.attachments?.[index]?.id;
        if (attachmentId !== undefined) {
          this.testService.deleteAttachment(this.testData.id, attachmentId).subscribe({
          next: (response) =>{
            if(response.isSuccessful){
              this.testData.attachments?.splice(index, 1);
            }
          },
          error: (error) =>{
            console.error(error);
          },
          complete: () => { }
        });
        return;
      }}else
      if(this.testData.attachments?.length==1){
        this.testData.attachments =[];
      }
      try{

        this.testData.attachments?.splice(index, 1);
      }catch(error){
        console.log(error);
        this.testData.attachments?.pop();

      }
    
    
  }
  addAttachment() {
    const fileInput = document.getElementById('attachment') as HTMLInputElement;
    fileInput.click();

    fileInput.onchange = () => {
      if (fileInput.files && fileInput.files.length > 0) {
        if (this.testData.id > 0) {
          this.testService.addAttachment(this.testData.id, fileInput.files[0]).then(observable => {
            observable.subscribe({
              next: (response: GenericResponse) => {
                if (response.isSuccessful) {
                  // Handle successful response
                  /* const attachment: Attachment = {
                    id: response.data.id,
                    name: response.data.name,
                    file: fileInput.files[0]
                  };
                  if (this.testData.attachments) {
                    this.testData.attachments.push(attachment);
                  } else {
                    this.testData.attachments = [attachment];
                  }*/
                }
              },
              error: (error: any) => {
                console.error(error);
              },
              complete: () => { }
            });
          }).catch(error => {
            console.error(error);
          });
        }
      }
    };
  }


  //#endregion





  
  //#region Equipments Form

  modalEquipments(){
    this.displayMaximizableEquipments =true;
    
  };

  onDeleteEquipment(_t116: Equipment) {
    if(this.testData.id>=0){
      this.testService.deleteEquipment(this.testData.id, _t116.id).subscribe({
        next: (response) =>{
          if(response.isSuccessful){
            this.testData.equipments = this.testData.equipments?.filter(x => x.id !== _t116.id);
          }
        },
        error: (error) =>{
          console.error(error);
        },
        complete: () => { }
      });
      return;
    }else
    this.testData.equipments = this.testData.equipments?.filter(x=>x!=_t116);
  }
    

  onSelectEquipment($equipment: Equipment) {
    console.log($equipment);
    this.displayMaximizableEquipments =false;

    if(this.testData.equipments&& !this.testData.equipments.find(x=>x==$equipment))
    {
        this.testData.equipments.push($equipment);
        return;
    }
    if(!this.testData.equipments){
      const equipments: Equipment[] =[
        $equipment,
      ]
      this.testData.equipments = equipments;
    }
  }
 

  //#endregion

  



  
  //#region Engineer Form

  modalEngs(){
    this.displayMaximizableEng =true;
    
  };

  onSelectEng($event: any) {
    console.log($event);
    this.displayMaximizableEng =false;
    this.testData.enginner = $event;
    
   }

   onDeleteEng(){
    this.testData.enginner = undefined;
   }


  //#endregion




  
  
  //#region  Technician Form

  modalTech(){
    this.displayMaximizableTech =true;
    
  };

  onSelectTech($event: any) {
    console.log($event);
    this.displayMaximizableTech =false;
    if(this.testData.technicians&&this.testData.technicians?.length>0){
      const tech = this.testData.technicians.find(x=>x.id== $event.id);
      if(tech){
        return
      }
      this.testData.technicians.push($event);
      return;
     
    }
    const users: Employee[] = [
      
        $event,
      
    ]
    this.testData.technicians = users;
    return;

   }
   onDeleteTech(arg0: number) {
    if(this.testData.id>=0){

      this.testService.deleteTechnician(this.testData.id, arg0).subscribe({
        next: (response) =>{
          if(response.isSuccessful){
            this.testData.technicians = this.testData.technicians?.filter(x => x.id !== arg0);
          }
        },
        error: (error) =>{
          console.error(error);
        },
        complete: () => { }
      });
      return;


    }else
    if (this.testData.technicians && this.testData.technicians.length > 0) {
      this.testData.technicians = this.testData.technicians.filter(x => x.id !== arg0);
    }
  }

  //#endregion






  //#region  Specification Form

  @Input()
  public EditAdd = signal<string>('');
  public specificationTemp: Specification ={
    details:'',
    specificationName:''
  }
  

  genericForm:GenericFormInterface<Specification>={
    tittle: '',
    fields: [],
    customFromGroup: undefined,
    editAdd: this.EditAdd(),
    customButton:'',
    data: this.specificationTemp
  };
  builderForm = new GenericFormConcretBuilder<Specification>();

  modalSpecifications(){
    this.displayMaximizableSpecifications = true;
  }


  onSelectSpecification($event: any){
    this.displayMaximizableSpecifications = false;
    if(!this.testData.specifications){
      const specifications: Specification[] =[        
          $event,        
      ];
      this.testData.specifications = specifications
      return;
    }
    if(this.testData.specifications.find(x=>x.specificationName == $event.specificationName)){
      return;
    }
    this.testData.specifications.push($event);
 
  }
  SubmitRequests() {
    const newSpecification: Specification ={
      details:'',
      specificationName:''
    };
    if (this.genericForm.customFromGroup && this.genericForm.customFromGroup.valid) {

      const formValues = this.genericForm.customFromGroup.value;
      newSpecification.details = formValues.details;
      newSpecification.specificationName = formValues.specificationName;

    }
     
    this.displayMaximizableSpecifications = false;
    if(!this.testData.specifications){
      const specifications: Specification[] =[        
        newSpecification,        
      ];
      this.testData.specifications = specifications
      return;
    }
    if(this.testData.specifications.find(x=>x.specificationName == newSpecification.specificationName)){
      return;
    }
    this.testData.specifications.push(newSpecification);
  }

  onDeleteSpecification(_t139: Specification) {
    if(this.testData.id>=0 && _t139.id){
      this.testService.deleteSpecification(this.testData.id, _t139.id).subscribe({
        next: (response) =>{
          if(response.isSuccessful){
            this.testData.specifications = this.testData.specifications?.filter(x => x.specificationName !== _t139.specificationName);
          }
        },
        error: (error) =>{
          console.error(error);
        },
        complete: () => { }
      });
      return;
    }else
    this.testData.specifications = this.testData.specifications?.filter(x=>x.specificationName != _t139.specificationName);
  }

  async ConfigForm() {         

    this.builderForm.Reset();
    this.builderForm.SetEditAdd(this.EditAdd().toString());


   
    this.builderForm.SetField({
      field: 'specificationName',
      label: 'Specification Name',
      order: 1,
      required: true,
      type: 'text',
      validationRequired: true,
      enable: true,
      show: true,
      value: this.specificationTemp.specificationName
    });
    this.builderForm.SetField({
      field: 'details',
      label: 'Details',
      order: 2,
      required: true,
      type: 'textArea',
      validationRequired: true,
      enable: true,
      show: true,
      value: this.specificationTemp.details
    });
   

    this.builderForm.SetFormGroup(
      this.fb.group({
        id: [0],
        name: '',
        email:'',
        employeeNumber:''   
      })
    );
    this.builderForm.SetSubmitFunction(
      () => {

        this.SubmitRequests();
      }
    );
    this.builderForm.SetTitle('Specification');
    this.genericForm = this.builderForm.Generate();

  }

  


  //#endregion




  //#region  Submit

  
  onSubmit() {
   

      if (this.testForm.valid) {
        const samples: Samples ={
          quantity: this.testForm.get('quantity')?.value,
          weight: this.testForm.get('weight')?.value,
          size:  this.testForm.get('size')?.value
        }
       
  
      
       


       
        if(this.EditAdd()=="Add"){

          const test: Test = {
            id: this.testForm.get('id')?.value,
            name: this.testForm.get('name')?.value,
            description: this.testForm.get('description')?.value,
            start: this.testForm.get('start')?.value ? new Date(this.testForm.get('start')?.value) : undefined,
            end: this.testForm.get('end')?.value ? new Date(this.testForm.get('end')?.value) : undefined,
            specialInstructions: this.testForm.get('specialInstructions')?.value,
            profile: this.testForm.get('profile')?.value, // Actualizado aquí            
            lastUpdatedMessage: 'Updated test form',
            samples: samples?? undefined,
            attachments: this.testData.attachments,
            enginner: this.testData.enginner,
            specifications: this.testData.specifications,
            technicians: this.testData.technicians,
            equipments: this.testData.equipments, 
            status: TestStatusEnum.New  
          };

          console.log(test);   
          
           // Emite el evento formSubmit con el formulario válido

          this.formSubmit.emit(test);


        }else{

          this.testData.name = this.testForm.get('name')?.value;
          this.testData.description = this.testForm.get('description')?.value;
          this.testData.start = this.testForm.get('start')?.value ? new Date(this.testForm.get('start')?.value) :          
          this.testData.end= this.testForm.get('end')?.value ? new Date(this.testForm.get('end')?.value) : undefined;
          this.testData.specialInstructions=this.testForm.get('specialInstructions')?.value;
          this.testData.profile=this.testForm.get('profile')?.value; // Actualizado aquí          
          this.testData.lastUpdatedMessage='Updated test form';
          this.testData.samples=samples?? undefined;            
          this.testData.status=this.convertToEnum(this.testForm.get('status')?.value??'NEW')??TestStatusEnum.New;
          this.testData.attachments=this.testData.attachments;
          this.testData.enginner=this.testData.enginner;
          this.testData.specifications=this.testData.specifications;
          this.testData.technicians=this.testData.technicians;
          this.testData.equipments=this.testData.equipments;

          console.log( this.testData ); 

          this.maximizeUpdate= true;
          
        

          
        }

      }
         
  }
  updateMessage($event: GenericUpdate) {   
    this.maximizeUpdate = false;
    this.currentUpdate= $event;
    if(this.testData.updates!=undefined){
      this.testData.updates.push(this.currentUpdate);
    }
    this.formSubmit.emit(this.testData);
  
    
  }
  

  





   
  
  onCancel() {
    throw new Error('Method not implemented.');
    }
    onClear() {
    throw new Error('Method not implemented.');
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

  convertToEnum(status: string): TestStatusEnum | undefined {
    const key = status.charAt(0).toUpperCase() + status.slice(1).toLowerCase(); // Capitaliza el valor del string
    return TestStatusEnum[key as keyof typeof TestStatusEnum]; // Devuelve el valor del enum o undefined si no existe
  }
    

  
}