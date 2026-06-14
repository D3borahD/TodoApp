export interface IProject {
  id: number
  label: string
  description: string;
  stepsList?: string[];
  startDate: Date;
  endDate?: Date;
  status: string;
}
