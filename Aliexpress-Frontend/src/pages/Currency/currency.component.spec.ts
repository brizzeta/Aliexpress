import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CurrencyComponent } from './currency.component';

describe('CurrencyComponent', () => {
  let component: CurrencyComponent;
  let fixture: ComponentFixture<CurrencyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CurrencyComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CurrencyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display available currencies', () => {
    const compiled = fixture.nativeElement;
    const currencyButtons = compiled.querySelectorAll('.currencies button');
    expect(currencyButtons.length).toBe(component.currencies.length);
  });

  it('should log currency change', () => {
    const currency = component.currencies[0];
    spyOn(console, 'log');
    component.changeCurrency(currency);
    expect(console.log).toHaveBeenCalledWith(`Currency changed to: ${currency.name}`);
  });
});
