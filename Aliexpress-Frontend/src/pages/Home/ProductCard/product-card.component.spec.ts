import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProductCardComponent } from './product-card.component';

describe('ProductCardComponent', () => {
  let component: ProductCardComponent;
  let fixture: ComponentFixture<ProductCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductCardComponent);
    component = fixture.componentInstance;

    // Пример данных о товаре для теста
    component.product = {
      name: 'Product Name',
      description: 'Product Description',
      price: 100,
      image: 'assets/product-image.jpg'
    };

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display product information', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('h3').textContent).toContain('Product Name');
    expect(compiled.querySelector('.product-description').textContent).toContain('Product Description');
    expect(compiled.querySelector('.product-price').textContent).toContain('100');
  });
});
