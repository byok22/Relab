import { ChangeDetectionStrategy, Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { GenericMenuItemComponent } from '../shared/components/generic-menu-item/generic-item-menu.component';
import { DropdownsService } from '../common/service/dropdowns.service';
import { GenericMenuInterface } from '../shared/components/generic-menu/interfaces/generic-menu-item.interface';
import { SelectOption } from '../shared/interfaces/select-option.interface';

@Component({
  selector: 'app-borrame',
  standalone: true,
  imports: [
    CommonModule,
    GenericMenuItemComponent,
    HttpClientModule
  ],
  providers: [
    DropdownsService
  ],
  template: `
  <div class="card">
    <div class="row menu m-0 menu">    
      <generic-item-menu [item]="menuItem"></generic-item-menu>
      <generic-item-menu [item]="menuItem2"></generic-item-menu>
    </div>
  </div>
  `,
  styleUrls: ['./borrame.component.css'],
  changeDetection: ChangeDetectionStrategy.Default,
})
export class BorrameComponent implements OnInit {  
  loaded: boolean = false;
  building: SelectOption[] = [];
  menuItem!: GenericMenuInterface;
  selectedBuilding: string = '1';
  building2: SelectOption[] = [];
  menuItem2!: GenericMenuInterface;
  selectedBuilding2: string = '1';

  constructor(private service: DropdownsService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.initMenu();
    this.fillMenu();
  }

  initMenu(): void {
    this.menuItem = { 
      item: { 
        options: this.building, 
        onChange: (event: string) => { 
          this.selectedBuilding = event;
          console.log('Selected option changed:', event);

          this.service.getStatus().subscribe({
            next: (status) => {
              
              this.building2 = status.filter(x => (x.text !== 'New' && x.text != 'All'));
              this.selectedBuilding2 =  this.building2[0]?.id || '1'; // Asegúrate de que selectedBuilding tiene un valor válido
              this.menuItem2.item.options = this.building2;
              this.menuItem2.item.selectedOption = this.selectedBuilding2;             
              //this.cdr.detectChanges(); // Marca el componente para la detección de cambios
            }
          });


        },
        selectedOption: this.selectedBuilding
      },
      labelText: 'Buildings',
      order: 1,
      type: 'dropdown'
    };
    this.menuItem2 = { 
      item: { 
        options: this.building2, 
        onChange: (event: string) => { 
          this.selectedBuilding2 = event;
          console.log('Selected option changed:', event);
        },
        selectedOption: this.selectedBuilding2
      },
      labelText: 'Buildings2',
      order: 1,
      type: 'dropdown'
    };
  }
  

  
  fillMenu(): void {
    this.service.getStatus().subscribe({
      next: (status) => {
        this.building = status;
        this.selectedBuilding = status[0]?.id || '1'; // Asegúrate de que selectedBuilding tiene un valor válido
        this.menuItem.item.options = this.building;
        this.menuItem.item.selectedOption = this.selectedBuilding;
        this.loaded = true;
        this.cdr.detectChanges(); // Marca el componente para la detección de cambios
      }
    });
  }
}
