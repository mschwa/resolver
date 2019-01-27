import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { MatSnackBar } from '@angular/material';

import { AccountService } from 'app/services/accout.service';

import { Account } from 'app/models/acccount';
import { Router } from '@angular/router';

@Component({
    selector     : 'login',
    templateUrl  : './login.component.html',
    styleUrls    : ['./login.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations   : fuseAnimations
})
export class LoginComponent implements OnInit
{
    loginForm: FormGroup;
    account: Account;
    
    constructor(
        private _fuseConfigService: FuseConfigService,
        private _formBuilder: FormBuilder,
        private _accountService: AccountService,
        private _snackBar: MatSnackBar,
        private _router: Router
    )
    {
        // Configure the layout
        this._fuseConfigService.config = {
            layout: {
                navbar   : {
                    hidden: true
                },
                toolbar  : {
                    hidden: true
                },
                footer   : {
                    hidden: true
                },
                sidepanel: {
                    hidden: true
                }
            }
        };
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {
        this.loginForm = this._formBuilder.group({
            username   : ['', [Validators.required, Validators.email]],
            password: ['', Validators.required]
        });
    }

    login(): void 
    {
        if (this.loginForm.valid) {
            
            this.account = Object.assign({}, this.loginForm.value);
               
            this._accountService.login(this.account).subscribe(
              (response: any) => {
                    const account = response;
                    localStorage.setItem('token', account.access_token);  
                    this._router.navigate(['/']);                                 
              },
              error => {
                  this._snackBar.open('Error', error, {
                      duration: 3000,
                    });
              }
            );
          }
    }
}
