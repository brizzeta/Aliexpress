import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BasketPaymentComponent } from './basket-payment.component';

describe('BasketComponent', () => {
  let component: BasketPaymentComponent;
  let fixture: ComponentFixture<BasketPaymentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({ imports: [BasketPaymentComponent] }).compileComponents();
    fixture = TestBed.createComponent(BasketPaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
  it('should create', () => { expect(component).toBeTruthy(); });
});