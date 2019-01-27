import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { FuseSharedModule } from '@fuse/shared.module';

import { RefercaseComponent } from './refercase.component';

const routes = [
    {
        path     : 'refercase',
        component: RefercaseComponent
    }
];

@NgModule({
    declarations: [
        RefercaseComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        TranslateModule,

        FuseSharedModule
    ],
    exports     : [
        RefercaseComponent
    ]
})

export class RefercaseModule
{
}
