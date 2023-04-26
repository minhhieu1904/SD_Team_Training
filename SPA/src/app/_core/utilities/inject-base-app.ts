import { inject } from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { DestroyService } from "@services/common/destroy.service";
import { NgSnotifyService } from '@services/common/ng-snotify.service';
import { FunctionUtility } from "@utilities/function-utility";
import { NgxSpinnerService } from "ngx-spinner";
export abstract class InjectBase {
    protected router = inject(Router);
    protected route = inject(ActivatedRoute);
    protected spinnerService = inject(NgxSpinnerService);
    protected snotifyService = inject(NgSnotifyService);
    protected destroyService = inject(DestroyService);
    protected functionUtility = inject(FunctionUtility);
    protected translateService = inject(TranslateService);



}
