import { CommonModule } from '@angular/common';
import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { PackingScanExportDTO, ViewDataPackingScan } from '@models/transaction/packing-scan';
import { PackingScanService } from '@services/packing-scan.service';
import { InjectBase } from '@utilities/inject-base-app';
import { OperationResult } from '@utilities/operation-result';
import { ModalDirective } from 'ngx-bootstrap/modal';
import {
  Config,
  NgxPrintElementModule,
  NgxPrintElementService,
} from 'ngx-print-element';
import { interval, take } from 'rxjs';

@Component({
  selector: 'app-packing-scan-export',
  templateUrl: './packing-scan-export.component.html',
  styleUrls: ['./packing-scan-export.component.scss'],
  standalone: true, // đối tượng độc lập
  imports: [CommonModule, NgxPrintElementModule],
})
export class PackingScanExportComponent extends InjectBase implements OnInit {
  constructor(public printService: NgxPrintElementService) {
    super();
  }

  data: PackingScanExportDTO[] = [];
  isCheck: boolean;

  ngOnInit() {}

  print(dataPrint: PackingScanExportDTO[], isCheck?: boolean) {
    this.data = dataPrint;
    this.isCheck = isCheck;
    let time = interval(1);

    console.log(this.data);


    time.pipe(take(1)).subscribe(() => {
      let config: Config = <Config>{
        printMode: 'template',
      };
      this.printService.print('printTemplate', config);
      document.getElementById('printTemplate').hidden = true;
    });
  }
}
