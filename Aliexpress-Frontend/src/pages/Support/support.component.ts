import { Component } from '@angular/core';

@Component({
  selector: 'app-support',
  standalone: true,
  imports: [],
  templateUrl: './support.component.html',
  styleUrls: ['./support.component.scss']
})
export class SupportComponent {
  issueDescription: string = '';

  submitRequest(event: Event) {
    event.preventDefault();
    console.log('Request submitted:', this.issueDescription);
    this.issueDescription = ''; // Clear the form after submission
  }
}
