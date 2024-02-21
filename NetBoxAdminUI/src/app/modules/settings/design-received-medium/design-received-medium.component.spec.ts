import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignReceivedMediumComponent } from './design-received-medium.component';

describe('DesignReceivedMediumComponent', () => {
  let component: DesignReceivedMediumComponent;
  let fixture: ComponentFixture<DesignReceivedMediumComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DesignReceivedMediumComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DesignReceivedMediumComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
