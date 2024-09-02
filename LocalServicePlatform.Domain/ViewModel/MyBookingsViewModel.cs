using LocalServicePlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Domain.ViewModel
{
    public class MyBookingsViewModel:BookingVM
    {
        
  
    public List<Bookings> UserBookings { get; set; }

    }

}

