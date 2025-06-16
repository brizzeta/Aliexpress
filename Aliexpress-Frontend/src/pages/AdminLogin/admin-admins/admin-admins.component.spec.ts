import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AdminAdminsComponent } from './admin-admins.component';

describe('AdminUsersComponent', () => {
  let component: AdminAdminsComponent;
  let fixture: ComponentFixture<AdminAdminsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({imports: [AdminAdminsComponent]}).compileComponents();
    fixture = TestBed.createComponent(AdminAdminsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
  it('should create', () => { expect(component).toBeTruthy(); });
});
