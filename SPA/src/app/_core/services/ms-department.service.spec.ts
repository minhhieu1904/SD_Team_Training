/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MsDepartmentService } from './ms-department.service';

describe('Service: MsDepartment', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MsDepartmentService]
    });
  });

  it('should ...', inject([MsDepartmentService], (service: MsDepartmentService) => {
    expect(service).toBeTruthy();
  }));
});
