import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CartComponent } from './cart.component';

describe('CartComponent', () => {
  let component: CartComponent;
  let fixture: ComponentFixture<CartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CartComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should calculate total price correctly', () => {
    const total = component.getTotal();
    expect(total).toBe(850); // 100 + 200*2 + 150
  });

  it('should remove item from cart', () => {
    const itemToRemove = component.cartItems[0];
    component.removeItem(itemToRemove);
    expect(component.cartItems.length).toBe(2);
  });

  it('should handle checkout correctly', () => {
    spyOn(console, 'log');
    component.checkout();
    expect(console.log).toHaveBeenCalledWith('Proceeding to checkout');
  });
});
