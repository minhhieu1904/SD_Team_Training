/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { Shift_dataService } from './shift_data.service';

describe('Service: Shift_data', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Shift_dataService]
    });
  });

  it('should ...', inject([Shift_dataService], (service: Shift_dataService) => {
    expect(service).toBeTruthy();
  }));
});
