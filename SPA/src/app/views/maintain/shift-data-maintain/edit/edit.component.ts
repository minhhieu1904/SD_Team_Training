import { Component, OnInit } from '@angular/core';
import { MsShift } from '@models/maintain/msShift';
import { ShiftDataMaintainService } from '@services/maintain/shift-data-maintain.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent extends InjectBase implements OnInit {

  msShift: MsShift = <MsShift>{
    manuf: 'N',
    shift: '',
    shiftName: ''
  };

  constructor(private service: ShiftDataMaintainService) { 
    super()
  }

  ngOnInit(): void {
    let manuf = '';
    let shift = '';

    this.route.params.subscribe(params => {
      manuf = params['manuf'];
      shift = params['shift'];
      this.getMsShift(manuf,shift)
    });
  }

  getMsShift(manuf : string, shift : string){
    this.service.getDataOnly(manuf,shift).subscribe({
      next: result =>{
        this.msShift = result;
      },
      error: (err) => console.log(err) 
    });

  }

  backlist() {
    this.router.navigate(["shift-data-maintain"])
  }

  update() {
    this.service.update(this.msShift).subscribe({
      next: res => { 
        if(res.isSuccess)
        {
          this.backlist();
        } else {
          alert('Cập nhật thất bại')
        }
      }
    })
  }
}
