import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { MS_Department } from '@models/common/mS_Department';
import { MSDepartmentService } from '@services/main/ms-department.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {
  msDepartment: MS_Department = <MS_Department>{
    manuf: '',
    parNo: '',
    parName: ''
  }


  constructor(
    private service: MSDepartmentService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    let manuf: '';
    let parNo: '';
    this.route.params.subscribe(params => {
      manuf = params['manuf'];
      parNo = params['parNo'];
      this.getItem(manuf, parNo);
    });
    this.updateDepartment();
  }

  backList() {
    this.router.navigate(['maintain/department-data-maintain'])
  }

  getItem(manuf: string, parNo: string) {
    this.service.getItem(manuf, parNo).subscribe({
      next: res => {
        this.msDepartment = res;
      }, error: (err) => { console.log(err) },
      complete: () => {
        console.log('complete');
      }
    })
  }

  updateDepartment() {
    this.service.updateItem(this.msDepartment).subscribe({
      next: res => {
        if (res.isSuccess)
          alert('Update thanh cong')
      },
      error: () => {
        alert('Update khong thanh cong')
      }
    })
  }

  updateDepartmentAndNext() {
    this.service.updateItem(this.msDepartment).subscribe({
      next: res => {
        if (res.isSuccess)
          alert('Update thanh cong')
      },
      error: () => {
        alert('Update khong thanh cong')
      },
      complete: () => {
        this.backList();
      }
    })
  }
}
