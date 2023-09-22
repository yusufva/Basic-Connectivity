using Basic_Connectivity.Models;
using Basic_Connectivity.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Connectivity.Controllers;

public class RegionController
{
    private Region _region;
    private RegionView _regionView;

    public RegionController(Region region, RegionView regionView)
    {
        _region = region;
        _regionView = regionView;
    }

    public void GetAll()
    {
        var result = _region.GetAll();
        if (!result.Any())
        {
            Console.WriteLine("No record found");
        }
        {
            _regionView.List(result, "regions");
        }
    }

    public void Insert()
    {
        string input = "";
        var isTrue = true;
        while (isTrue)
        {
            try
            {
                input = _regionView.InsertInput();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Region Name cannot be empty!");
                    continue;
                }
                isTrue = false;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        var result = _region.Insert(new Region
        {
            Id = 0,
            Name = input,
        });
        _regionView.Message(result);
    }

    public void Update()
    {
        var region = new Region();
        var isTrue = true;
        while (isTrue)
        {
            try
            {
                region = _regionView.UpdateRegion();
                if (string.IsNullOrEmpty(region.Name))
                {
                    Console.WriteLine("Region Name cannot be empty");
                    continue;
                }
                isTrue = false;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        var result = _region.Update(region);
        _regionView.Message(result);
    }

    public void Delete()
    {
        var region = new Region();
        var isTrue = true;
        while (isTrue)
        {
            try
            {
                region = _regionView.DeleteInput();
                var data = _region.GetById(region.Id);
                if (string.IsNullOrEmpty(data.Name)){
                    Console.WriteLine($"No Region with Id = {data.Id}");
                    continue;
                }
                isTrue = false;
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        var result = _region.Delete(region.Id);
        _regionView.Message(result);
    }
        
}

