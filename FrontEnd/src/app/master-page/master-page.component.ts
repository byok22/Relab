import { CommonModule } from '@angular/common';
import {  Component, OnInit, signal  } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { GenericTableComponent } from '../shared/components/generic-table/generic-table.component';

import { GenericMenuComponent } from '../shared/components/generic-menu/generic-menu.component';

import { SelectDropdownComponent } from '../shared/components/select-dropdown/select-dropdown.component';

import { PrimengModule } from '../shared/modules/primeng.module';
import { GenericFormComponent } from '../shared/components/generic-form/generic-form.component';

import { FullCalendarComponent } from '../shared/components/full-calendar/full-calendar.component';

import { IBody, IMasterPage } from './builder/master-page.interface';
import { MasterPageConcretBuilder } from './builder/master-page-concret-builder';
import { BodyComponent } from './components/body/body.component';
import { SidenavComponent } from './components/sidenav/sidenav.component';
import { TestRequestsComponent } from '../test-requests/page/test-requests.page';

@Component({
  selector: 'app-master-page',
  standalone: true,
  imports: [
    CommonModule,RouterOutlet, GenericTableComponent,
     GenericMenuComponent, SelectDropdownComponent, PrimengModule, GenericFormComponent
    ,FullCalendarComponent, BodyComponent
    ,SidenavComponent, TestRequestsComponent
  ],
  templateUrl: './master-page.component.html',
  styleUrl: './master-page.component.css'
})
export class MasterPageComponent implements OnInit  {

  //Page Config

 
   //#region Master Page Declarations
   master: IMasterPage = {
    body: {
        collapsed:false,
        screenWidth:0
    }
  };
  //Body
  body: IBody = {
  collapsed:true,
  screenWidth:0
  };
  collapsed: boolean = false;
  screenWidth: number = 0;

  //#endregion

  
  constructor(
  ) {
  
    
  }

  ngOnInit(): void{
    this.ConfigMaster();
 
    

  }
  ConfigMaster(){
    this.ConfigBody();
  }
  ConfigBody(){
    this.body.collapsed = this.collapsed;
    this.body.screenWidth = this.screenWidth;
    const builder = new MasterPageConcretBuilder();
    builder.setBody(this.body);
    this.master = builder.Generate();    
  }
  
}