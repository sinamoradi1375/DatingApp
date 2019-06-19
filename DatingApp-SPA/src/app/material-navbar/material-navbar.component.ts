import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-material-navbar',
  templateUrl: './material-navbar.component.html',
  styleUrls: ['./material-navbar.component.css']
})
export class MaterialNavbarComponent implements OnInit {
  isHandset: Observable<BreakpointState> = this.breakpointObserver.observe(Breakpoints.Handset);
  model: any = {};
  photoUrl: string;
  hide = true;

  constructor(private breakpointObserver: BreakpointObserver, public authService: AuthService,
     private alertify: AlertifyService, private router: Router) {}

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe(res => this.photoUrl = res);
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('ورود با موفقیت انجام شد.');
      this.router.navigate(['members']);
    }, error => {
      this.alertify.error(error);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.message('با موفقیت خارج شدید.');
    this.router.navigate(['']);
  }

}
