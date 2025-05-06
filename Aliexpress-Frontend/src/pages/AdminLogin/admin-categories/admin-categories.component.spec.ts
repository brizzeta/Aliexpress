import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AdminCategoriesComponent } from './admin-categories.component';

describe('AdminUsersComponent', () => {
  let component: AdminCategoriesComponent;
  let fixture: ComponentFixture<AdminCategoriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({imports: [AdminCategoriesComponent]}).compileComponents();
    fixture = TestBed.createComponent(AdminCategoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
  it('should create', () => { expect(component).toBeTruthy(); });
});
