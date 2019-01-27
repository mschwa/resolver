/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AccoutService } from './accout.service';

describe('Service: Accout', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AccoutService]
    });
  });

  it('should ...', inject([AccoutService], (service: AccoutService) => {
    expect(service).toBeTruthy();
  }));
});
