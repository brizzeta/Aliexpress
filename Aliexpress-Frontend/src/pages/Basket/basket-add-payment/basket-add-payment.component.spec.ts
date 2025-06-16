import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BasketAddPaymentComponent } from './basket-add-payment.component';

describe('BasketComponent', () => {
  let component: BasketAddPaymentComponent;
  let fixture: ComponentFixture<BasketAddPaymentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({ imports: [BasketAddPaymentComponent] }).compileComponents();
    fixture = TestBed.createComponent(BasketAddPaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
  it('should create', () => { expect(component).toBeTruthy(); });
});