import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EasyestimateComponent } from './easyestimate.component';

describe('EasyestimateComponent', () => {
  let component: EasyestimateComponent;
  let fixture: ComponentFixture<EasyestimateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EasyestimateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EasyestimateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
