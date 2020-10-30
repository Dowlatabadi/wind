using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LinePoint;
using UnityEngine;

namespace Classes
{
    public enum game_mode
    {
        millCreataion,
        pivotCreation
    }

    public enum Pivot_type
    {
        ClockWise,
        CounterClockWise
    }

    public class Level
    {
        public game_mode gamemode { get; set; }

        public List<(Vector2 pivot_pos, Pivot_type pivot_type, bool labeled)>
        Pivots { get; set; }

        public string Info { get; set; }

        public int Known_pivots { get; set; }

        public Level(
            game_mode game_mode,
            List<(Vector2 pivot_pos, Pivot_type pivot_type, bool labeled)>
            Pivots,
            string Info,
            int Known_pivots
        )
        {
            this.gamemode = game_mode;
            this.Pivots = Pivots;
            this.Info = Info;
            this.Known_pivots = Known_pivots;
        }

        public Level(int number)
        {
             var res =
                new List<(
                        Vector2 pivot_pos,
                        Pivot_type pivot_type,
                        bool labeled
                    )
                >();
            List<Vector2> pivots_pos = new List<Vector2>();
            for (int i = 0; i < number; i++)
            {
                (
                    Vector2 pivot_pos,
                    Pivot_type pivot_type,
                    bool labeled
                ) cur_tuple;
                var point =
                    Helper.find_next_point(res.Select(x => x.pivot_pos).ToList(),
                    .4f);
                cur_tuple.pivot_pos = point;

                //rotation
                if (Random.value > 0.5f)
                    cur_tuple.pivot_type = Pivot_type.ClockWise;
                else
                    cur_tuple.pivot_type = Pivot_type.CounterClockWise;

                if (Random.value > 0.5f)
                {
                    cur_tuple.labeled = true;
                }
                else
                {
                    cur_tuple.labeled = false;
                }
                res.Add (cur_tuple);
            }
            Pivots = res;
        }

     
    }
}
