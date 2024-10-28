import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CardInterface } from '../../interfaces/cards.interface';
import { CardServiceService } from '../../services/card-service.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'presentation-page',
  standalone: true,
  imports:[CommonModule],
  templateUrl: './presentation-page.component.html',
  styleUrls: ['./presentation-page.component.scss']
})
export class PresentationPageComponent implements OnInit {
  visibleCards: CardInterface[] = [];
  currentPage = 0;
  hasMoreItems = true;
  fetchedCards: CardInterface[]=[];

  // Define las distancias para el desplazamiento infinito
  scrollDistance = 2;
  scrollUpDistance = 1.5;

  constructor(private cardService: CardServiceService, private router: Router) {
    this.cardService.initCards();

   this.fetchedCards=[
      {
        tittle: 'Tests Requests',
        targetUrl: '/request',
        imgSrc: 'assets\\img\\presentation\\Test-requests.jpg',
        desc:'Requests'
      },
      {
        tittle: 'Calendar',
        targetUrl: '/calendar',
        imgSrc: 'assets\\img\\presentation\\Calendar.jpg',
        desc:'Calendar'
      },
      
      {
        tittle: 'User',
        targetUrl: '/users',
        imgSrc: 'assets\\img\\presentation\\Users.jpg',
        desc:'Catalogs'
      },
      
      {
        tittle: 'Equipments',
        targetUrl: '/equipments',
        imgSrc: 'assets\\img\\presentation\\Equipments.jpg',
        desc:'Catalogs'
      },
    
      // ... tus tarjetas
    ];
    this.cardService.setCards(this.fetchedCards);

  }

  ngOnInit(): void {
    console.log("Entrando a on Init");
    
    // Supongamos que obtienes las tarjetas de algún lugar (por ejemplo, una API)
    

    // Asigna las tarjetas al servicio
   
    // Carga las primeras tarjetas
    this.loadMore();
  }

  loadMore() {
    console.log("Load more function called");
    this.cardService.getCards(this.currentPage).subscribe(newCards => {
      if (newCards.length > 0) {
        this.visibleCards = [...this.visibleCards, ...newCards];
        this.currentPage++;
      } else {
        this.hasMoreItems = false;
      }
    });
  }
  onScroll() {
    console.log("scrolled!!");
  }
  visitPage(url: string): void {
    // Aquí puedes realizar la redirección a la URL específica de la tarjeta
    // Por ejemplo, podrías usar el Router de Angular:
    this.router.navigate([url]);
  }
}
