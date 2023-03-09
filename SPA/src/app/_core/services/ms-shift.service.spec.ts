/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MsShiftService } from './ms-shift.service';

describe('Service: MsShift', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MsShiftService]
    });
  });

  it('should ...', inject([MsShiftService], (service: MsShiftService) => {
    expect(service).toBeTruthy();
  }));
});
