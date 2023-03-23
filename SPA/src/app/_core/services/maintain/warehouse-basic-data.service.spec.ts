/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WarehouseBasicDataService } from './warehouse-basic-data.service';

describe('Service: WarehouseBasicData', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WarehouseBasicDataService]
    });
  });

  it('should ...', inject([WarehouseBasicDataService], (service: WarehouseBasicDataService) => {
    expect(service).toBeTruthy();
  }));
});
