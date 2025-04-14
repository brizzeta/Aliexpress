import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DiscountsComponent } from './discounts.component';
import { ProductCardComponent } from '../product-card/product-card.component';

describe('DiscountsComponent', () => {
  let component: DiscountsComponent;
  let fixture: ComponentFixture<DiscountsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DiscountsComponent, ProductCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DiscountsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});