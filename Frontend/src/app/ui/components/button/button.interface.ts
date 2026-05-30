export interface IButton {
  label: string;
  icon?: string;
  action: () => void;
  isDisabled: boolean;
}
