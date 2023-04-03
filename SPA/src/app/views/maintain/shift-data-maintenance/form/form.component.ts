import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Route, Router } from '@angular/router';
import { IconButton } from '@constants/common.constants';
import { MS_Shift } from '@models/maintain/msshift';
import { ShiftDataMaintenanceService } from '@services/shift-data-maintenance.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {
  icon = IconButton;
  title: string;

  paramData: MS_Shift = <MS_Shift>{
    nanuf: 'N',
    shift: '',
    shiftName: '',
  };

  constructor(
    private service: ShiftDataMaintenanceService,
    private router: Router
  ) {}

  ngOnInit() {
    if (this.router.url === '/maintain/shift-data-maintenance/edit') {
      this.title = 'Edit';

      var inputShift = document.querySelector('#shift');
      inputShift.setAttribute('disabled', 'disabled');

      this.service.currentDataSource.subscribe((res) => {
        if (res) {
          (this.paramData.shift = res.shift),
            (this.paramData.shiftName = res.shiftName);
        }
      });
      var a = this.paramData;
    } else {
      this.title = 'AddNew';
    }
  }

  redirectAdd() {
    if (this.router.url === '/maintain/shift-data-maintenance/edit') {
      console.log(this.paramData);

      this.service.update(this.paramData).subscribe({
        next: (res) => {
          console.log(res);
          this.router.navigate(['maintain/shift-data-maintenance']);
        },
        error: (err) => {},
      });
    }

    if (this.router.url === '/maintain/shift-data-maintenance/add') {
      this.service.create(this.paramData).subscribe({
        next: (res) => {
          console.log(res);
          this.router.navigate(['maintain/shift-data-maintenance']);
        },
        error: (err) => {},
      });
    }
  }

  backHome() {
    this.router.navigate(['maintain/shift-data-maintenance']);
  }
}
