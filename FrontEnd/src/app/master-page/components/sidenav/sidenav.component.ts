import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { NavStatus } from './enums/nav-status.enum';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FontAwesomeIcons } from './enums/font-aswesome-icons.enum';
import { NavItem } from './interfaces/nav-item.interface';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { animate, style, transition, trigger } from '@angular/animations';
import { icon, IconDefinition } from '@fortawesome/fontawesome-svg-core';
import { faThumbtack, faCircle, faAngleRight, faAngleDown, faArrowRight, faArrowLeft, faArrowUp, faArrowDown, faCheck, faCheckCircle, faTimes, faTimesCircle, faExclamation, faExclamationCircle, faQuestion, faQuestionCircle, faPlus, faMinus, faTrash, faEdit, faSave, faUpload, faDownload, faUser, faUsers, faLock, faUnlock, faCog, faWrench, faHome, faBell, faEnvelope, faComment, faComments, faHeart, faStar, faCamera, faImage, faPlay, faPause, faStop, faForward, faBackward, faSearch, faMap, faGlobe, faHomeUser, faT, faC, faA } from '@fortawesome/free-solid-svg-icons';
import { SublevelMenuComponent } from './sublevel-menu/sublevel-menu.component';
@Component({
  selector: 'sidenav-bar',
  standalone: true,
  imports: [
    CommonModule,
    FontAwesomeModule,
    RouterLink,
    RouterLinkActive,
    SublevelMenuComponent
  ],
  animations: [  trigger('fadeInOut', [
    transition(':enter', [
      style({opacity: 0}),
      animate('500ms',
        style({opacity: 1})
      )
    ]),
    transition(':leave', [
      style({opacity: 1}),
      animate('500ms',
        style({opacity: 0})
      )
    ])
  ])],
  templateUrl: './sidenav.component.html',
  styleUrl: './sidenav.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SidenavComponent {  
  /**
   *
   */
  Iconos: { [key in FontAwesomeIcons]: IconDefinition } = {
    [FontAwesomeIcons.faThumbtack]: faThumbtack,
    [FontAwesomeIcons.faCircle]: faCircle,
    [FontAwesomeIcons.faAngleRight]: faAngleRight,
    [FontAwesomeIcons.faAngleDown]: faAngleDown,
    [FontAwesomeIcons.faArrowRight]: faArrowRight,
    [FontAwesomeIcons.faArrowLeft]: faArrowLeft,
    [FontAwesomeIcons.faArrowUp]: faArrowUp,
    [FontAwesomeIcons.faArrowDown]: faArrowDown,
    [FontAwesomeIcons.faCheck]: faCheck,
    [FontAwesomeIcons.faCheckCircle]: faCheckCircle,
    [FontAwesomeIcons.faTimes]: faTimes,
    [FontAwesomeIcons.faTimesCircle]: faTimesCircle,
    [FontAwesomeIcons.faExclamation]: faExclamation,
    [FontAwesomeIcons.faExclamationCircle]: faExclamationCircle,
    [FontAwesomeIcons.faQuestion]: faQuestion,
    [FontAwesomeIcons.faQuestionCircle]: faQuestionCircle,
    [FontAwesomeIcons.faPlus]: faPlus,
    [FontAwesomeIcons.faMinus]: faMinus,
    [FontAwesomeIcons.faTrash]: faTrash,
    [FontAwesomeIcons.faEdit]: faEdit,
    [FontAwesomeIcons.faSave]: faSave,
    [FontAwesomeIcons.faUpload]: faUpload,
    [FontAwesomeIcons.faDownload]: faDownload,
    [FontAwesomeIcons.faUser]: faUser,
    [FontAwesomeIcons.faUsers]: faUsers,
    [FontAwesomeIcons.faLock]: faLock,
    [FontAwesomeIcons.faUnlock]: faUnlock,
    [FontAwesomeIcons.faCog]: faCog,
    [FontAwesomeIcons.faWrench]: faWrench,
    [FontAwesomeIcons.faHome]: faHome,
    [FontAwesomeIcons.faBell]: faBell,
    [FontAwesomeIcons.faEnvelope]: faEnvelope,
    [FontAwesomeIcons.faComment]: faComment,
    [FontAwesomeIcons.faComments]: faComments,
    [FontAwesomeIcons.faHeart]: faHeart,
    [FontAwesomeIcons.faStar]: faStar,
    [FontAwesomeIcons.faCamera]: faCamera,
    [FontAwesomeIcons.faImage]: faImage,
    [FontAwesomeIcons.faPlay]: faPlay,
    [FontAwesomeIcons.faPause]: faPause,
    [FontAwesomeIcons.faStop]: faStop,
    [FontAwesomeIcons.faForward]: faForward,
    [FontAwesomeIcons.faBackward]: faBackward,
    [FontAwesomeIcons.faSearch]: faSearch,
    [FontAwesomeIcons.faMap]: faMap,
    [FontAwesomeIcons.faGlobe]: faGlobe,
    [FontAwesomeIcons.faHomeUser]: faHomeUser,
    [FontAwesomeIcons.faT]: faT,
    [FontAwesomeIcons.faC]: faC,
    [FontAwesomeIcons.faA]: faA,
  };
  constructor(
    private _router: Router
  ) {
    
    this.SetSCSSProperties();
    this.prueba();
    
  }


  //#region  SIDENAV DIV
  //Config Div sidenav
  @Input() expandWidth: string = "17rem";
  @Input() backgroundColor: string = "rgb(0, 56, 101)";
  @Input() fontColor: string = "#ffffff";
  @Input() status: string = "production";
  
  marginWidth: string = "3rem";

  navStatus: NavStatus = NavStatus.Collapsed;  

  GetNavStatus(): string{
    if(this.navStatus == NavStatus.Collapsed){
      return 'sidenav-collapsed';
    }
    return '';
  }
  Enter():void{
    if (this.navStatus != "Pinned") {
      this.navStatus = NavStatus.Expand;
      localStorage.setItem("ot-sidenav-status", this.navStatus);
    }
  }

  Leave():void{
    if (this.navStatus != "Pinned") {
      this.navStatus = NavStatus.Collapsed;
      localStorage.setItem("ot-sidenav-status", this.navStatus);
    }
  }
  SetSCSSProperties(): void {
    document.documentElement.style.setProperty(
      "--expand-width-ot-sidenav",
      this.expandWidth
    );
    document.documentElement.style.setProperty(
      "--background-color-ot-sidenav",
      this.backgroundColor
    );
    document.documentElement.style.setProperty(
      "--font-color-ot-sidenav",
      this.fontColor
    );
    document.documentElement.style.setProperty(
      "--margin-width-app-content",
      this.marginWidth
    );
  }
  //#endregion

  //#region  HEADER

  GetPinnedClass():string{
    if (this.navStatus == NavStatus.Pinned){
      return '';
    }
    return 'rotate-pin-button'
  }

  togglePinned(): void {
    if (this.navStatus == NavStatus.Pinned) {
      this.navStatus = NavStatus.Expand;
      this.marginWidth = "3rem";
    }
    else {
      this.navStatus = NavStatus.Pinned;
      this.marginWidth = this.expandWidth;
    }
    this.SetSCSSProperties();
    localStorage.setItem("ot-sidenav-status", this.navStatus);
    return;
  }
  
  //#endregion

  //#region BODY

  items: NavItem[] = [];
  filteredItems: NavItem[] = [];
  multiple: boolean = false;

  shrinkOrExpand(item: NavItem): void {
    this.shrinkItems(item);
    item.expanded = !item.expanded;
  }
  shrinkItems(item: NavItem): void {
    if (!this.multiple) {
      for (let modelItem of this.items) {
        if (item !== modelItem && modelItem.expanded) {
          modelItem.expanded = false;
        }
      }
    }
  }
  getActiveClass(item: NavItem): string {
      if (this._router.url.includes(item.href) && item.href && item.href != "") {
          return "active";
      }
      
      if (!item.href && item.expanded && item.href == "") {
          return "active";
      }
      
      if (item.childrens) {
          return item.childrens.some(child => this._router.url.includes(child.href)) ? "active" : "";
      }

      return "";
  }
  
  GetIcon(item:FontAwesomeIcons|undefined, defaul?:IconDefinition): IconDefinition{
    if(!item&&defaul)
      return defaul;

    if(!item)
      return this.Iconos.faCircle;

    const icon = this.Iconos[item];
  if (!icon) {
    this.Iconos.faCircle;
  }
  return icon;

    
  }



  //#endregion

  prueba():void{
    this.items = [
      {name:'Prueba',
        href:'/request'
      },
      {name:'Prueba2',
        href:'/',
        
        
      }
    ];
    this.filteredItems = [
      {name:'Home',
        href:'/', 
        icon: FontAwesomeIcons.faHome
      },
      {name:'Tests',
        href:'/',
        icon: FontAwesomeIcons.faT,
        childrens: [
          {
            name:'Tests Process',
            href:'tests'

          },
          {name:'Request',
            href:'/request'
          },
          {name:'Calendar',
            href:'/calendar'
          },        
        ]
      },
      {name:'Catalogs',
        href:'/',
        icon: FontAwesomeIcons.faC,
        childrens: [
          {name:'Equipments',
            href:'/equipments'
          },
           {name:'Employees',
             href:'/employees'
           },
        ]
      },
      {name:'About',
        href:'/about',
        icon: FontAwesomeIcons.faA
      }
    ];;
  }
}
