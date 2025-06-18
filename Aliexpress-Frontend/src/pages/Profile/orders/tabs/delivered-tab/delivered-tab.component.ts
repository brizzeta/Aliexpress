import { Component, ElementRef, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { RouterModule, Router, NavigationEnd, Event as RouterEvent } from '@angular/router';
import { CommonModule } from '@angular/common';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-delivered-tab',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './delivered-tab.component.html',
  styleUrls: ['./delivered-tab.component.scss']
})
export class DeliveredTabComponent implements AfterViewInit, OnInit {
  @ViewChild('slider', { static: false }) slider!: ElementRef;
  public showDeliveredTab: boolean = true;
  public isNoactiveRoute: boolean = false; // Добавляем переменную для маршрута /noactive

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.checkRoute();

    this.router.events
      .pipe(
        filter((event: RouterEvent): event is NavigationEnd => event instanceof NavigationEnd)
      )
      .subscribe((event: NavigationEnd) => {
        this.checkRoute();
      });
  }

  private checkRoute(): void {
    const url = this.router.url;
    // Показываем карточки доставленных заказов только если нет дочерних маршрутов (track, review, return-refund, buy-again, noactive)
    this.showDeliveredTab = !url.includes('track') && 
                           !url.includes('review') && 
                           !url.includes('return-refund') && 
                           !url.includes('buy-again') && 
                           !url.includes('noactive');
    this.isNoactiveRoute = url.includes('noactive');
  }

  ngAfterViewInit() {
    if (this.slider) {
      const sliderContent = this.slider.nativeElement.querySelector('.slider-content');
      if (sliderContent) {
        sliderContent.addEventListener('scroll', () => this.updateArrowVisibility());
      }
    }
  }

  scrollRight() {
    if (this.slider) {
      const sliderContent = this.slider.nativeElement.querySelector('.slider-content');
      if (sliderContent) {
        sliderContent.scrollBy({
          left: 280,
          behavior: 'smooth'
        });
        this.updateArrowVisibility();
      }
    }
  }

  scrollLeft() {
    if (this.slider) {
      const sliderContent = this.slider.nativeElement.querySelector('.slider-content');
      if (sliderContent) {
        sliderContent.scrollBy({
          left: -280,
          behavior: 'smooth'
        });
        this.updateArrowVisibility();
      }
    }
  }

  updateArrowVisibility() {
    if (this.slider) {
      const sliderContent = this.slider.nativeElement.querySelector('.slider-content');
      const leftArrow = this.slider.nativeElement.querySelector('.slider-arrow-left');
      if (sliderContent && leftArrow) {
        if (sliderContent.scrollLeft > 0) {
          leftArrow.style.display = 'block';
        } else {
          leftArrow.style.display = 'none';
        }
      }
    }
  }

  // Обработчик события активации router-outlet
  onOutletActivated(component: any) {
    this.isNoactiveRoute = !!component; // Если компонент загружен в router-outlet, это noactive
    this.showDeliveredTab = !this.isNoactiveRoute; // Скрываем основной контент, если загружен noactive
  }
}