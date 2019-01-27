import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RefercaseComponent } from './refercase.component';

describe('RefercaseComponent', () => {
  let component: RefercaseComponent;
  let fixture: ComponentFixture<RefercaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RefercaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RefercaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
