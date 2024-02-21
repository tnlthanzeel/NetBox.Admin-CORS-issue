import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserRoleViewComponent } from './user-role-view.component';

describe('UserRoleViewComponent', () => {
  let component: UserRoleViewComponent;
  let fixture: ComponentFixture<UserRoleViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserRoleViewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserRoleViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
