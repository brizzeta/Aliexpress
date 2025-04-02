import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LanguageComponent } from './language.component';

describe('LanguageComponent', () => {
  let component: LanguageComponent;
  let fixture: ComponentFixture<LanguageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LanguageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LanguageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display available languages and currencies', () => {
    const compiled = fixture.nativeElement;
    const languageButtons = compiled.querySelectorAll('.languages button');
    const currencyButtons = compiled.querySelectorAll('.currencies button');
    expect(languageButtons.length).toBe(component.languages.length);
    expect(currencyButtons.length).toBe(component.currencies.length);
  });

  it('should log language change', () => {
    const language = component.languages[0];
    spyOn(console, 'log');
    component.changeLanguage(language);
    expect(console.log).toHaveBeenCalledWith(`Language changed to: ${language.name}`);
  });

  it('should log currency change', () => {
    const currency = component.currencies[0];
    spyOn(console, 'log');
    component.changeCurrency(currency);
    expect(console.log).toHaveBeenCalledWith(`Currency changed to: ${currency.name}`);
  });
});
