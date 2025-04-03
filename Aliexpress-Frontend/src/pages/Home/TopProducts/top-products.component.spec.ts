import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TopProductsComponent } from './top-products.component';
import { ProductCardComponent } from '../product-card/product-card.component';

describe('TopProductsComponent', () => {
  let component: TopProductsComponent;
  let fixture: ComponentFixture<TopProductsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TopProductsComponent, ProductCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TopProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display top products', () => {
    const compiled = fixture.nativeElement;
    const productCards = compiled.querySelectorAll('app-product-card');
    expect(productCards.length).toBe(component.products.length);
  });
});
