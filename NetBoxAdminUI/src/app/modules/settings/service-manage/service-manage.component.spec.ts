import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceManageComponent } from './service-manage.component';

describe('ServiceManageComponent', () => {
  let component: ServiceManageComponent;
  let fixture: ComponentFixture<ServiceManageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ServiceManageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ServiceManageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
