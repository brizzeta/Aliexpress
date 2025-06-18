import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-profile',
  standalone: true,
  imports: [],
  templateUrl: './admin-profile.component.html',
  styleUrl: './admin-profile.component.scss'
})
export class AdminProfileComponent implements OnInit {
  ngOnInit(): void { document.body.style.overflow = 'hidden'; }
}