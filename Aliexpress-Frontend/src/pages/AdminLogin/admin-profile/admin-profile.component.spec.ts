import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AdminProfileComponent } from './admin-profile.component';

describe('AdminUsersComponent', () => {
  let component: AdminProfileComponent;
  let fixture: ComponentFixture<AdminProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({imports: [AdminProfileComponent]}).compileComponents();
    fixture = TestBed.createComponent(AdminProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
  it('should create', () => { expect(component).toBeTruthy(); });
});