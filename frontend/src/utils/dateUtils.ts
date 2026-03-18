export const formatDateRange = (beginDate: string, endDate: string): string => {
  // Formata a data no padrão brasileiro sem afetar o fuso
  const format = (dateStr: string) => {
    const [_, month, day] = dateStr.split('-');
    return `${day}/${month}`;
  };

  return `${format(beginDate)} - ${format(endDate)}`;
};

export const formatDateWithWeekday = (dateStr: string): string => {
  const [year, month, day] = dateStr.split('-');
  const date = new Date(parseInt(year), parseInt(month) - 1, parseInt(day));
  
  return date.toLocaleDateString('pt-BR', { 
    weekday: 'long',
    day: '2-digit',
    month: '2-digit'
  });
};

export const isDateInCurrentWeek = (beginDate: string, endDate: string): boolean => {
  const today = new Date();
  today.setHours(0, 0, 0, 0);
  
  const [startYear, startMonth, startDay] = beginDate.split('-');
  const [endYear, endMonth, endDay] = endDate.split('-');
  
  const start = new Date(parseInt(startYear), parseInt(startMonth) - 1, parseInt(startDay));
  const end = new Date(parseInt(endYear), parseInt(endMonth) - 1, parseInt(endDay));
  
  return today >= start && today <= end;
};