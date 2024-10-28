import { NgModule } from '@angular/core';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { MultiSelectModule } from 'primeng/multiselect';
import { ProgressBarModule } from 'primeng/progressbar';
import { TagModule } from 'primeng/tag';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { CalendarModule } from 'primeng/calendar';
import { CheckboxModule } from 'primeng/checkbox';
import { MessagesModule } from 'primeng/messages';
import {  OverlayPanelModule } from 'primeng/overlaypanel';
import { CardModule } from 'primeng/card';



@NgModule({
  exports: [   
    MultiSelectModule, 
    PaginatorModule,
    TableModule,
    ProgressBarModule,
    TagModule,
    ButtonModule,
    DialogModule, 
    CalendarModule,
    CheckboxModule ,
    MultiSelectModule,
    MessagesModule,
    OverlayPanelModule,
    CardModule,

 
       
  ]
})
export class PrimengModule { }
