import { TestBed } from '@angular/core/testing';

import { WareHouseBasicDataService } from './warehouse-basic-data.service';

describe('WareHouseBasicDataService', () => {
  let service: WareHouseBasicDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WareHouseBasicDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
