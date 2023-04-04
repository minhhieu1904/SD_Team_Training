/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WkshSumReportService } from './wksh-sum-report.service';

describe('Service: WkshSumReport', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WkshSumReportService]
    });
  });

  it('should ...', inject([WkshSumReportService], (service: WkshSumReportService) => {
    expect(service).toBeTruthy();
  }));
});
