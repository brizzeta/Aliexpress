import { Component, ElementRef, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { RouterModule, Router, NavigationEnd, Event as RouterEvent } from '@angular/router';
import { CommonModule } from '@angular/common';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-orders-tab',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './orders-tab.component.html',
  styleUrls: ['./orders-tab.component.scss']
})
export class OrdersTabComponent implements AfterViewInit, OnInit {
  @ViewChild('slider', { static: false }) slider!: ElementRef;
  public showOrdersTab: boolean = true; // Добавляем переменную для управления отображением карточек

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
    // Показываем карточки заказов только если нет дочерних маршрутов (track, review, return-refund, buy-again)
    this.showOrdersTab = !url.includes('track') && 
                        !url.includes('review') && 
                        !url.includes('return-refund') && 
                        !url.includes('buy-again');
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
}