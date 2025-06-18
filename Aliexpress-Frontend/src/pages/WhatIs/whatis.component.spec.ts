import { ComponentFixture, TestBed } from '@angular/core/testing';
import { WhatIsComponent } from './whatis.component';

describe('BasketComponent', () => {
  let component: WhatIsComponent;
  let fixture: ComponentFixture<WhatIsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({ imports: [WhatIsComponent] }).compileComponents();
    fixture = TestBed.createComponent(WhatIsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
  it('should create', () => { expect(component).toBeTruthy(); });
});