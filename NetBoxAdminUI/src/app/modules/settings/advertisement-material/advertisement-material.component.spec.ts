import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvertisementMaterialComponent } from './advertisement-material.component';

describe('AdvertisementMaterialComponent', () => {
  let component: AdvertisementMaterialComponent;
  let fixture: ComponentFixture<AdvertisementMaterialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdvertisementMaterialComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdvertisementMaterialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
