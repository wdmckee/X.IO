using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace X.IO.Common.Stack
{
    public class Stack
    {

  
        
        public List<dynamic> ActionList = new List<dynamic>();



        //public void Result(dynamic result)
        //{

           
        //    if (((Result)result).arith_expression != null)
        //    {
        //        ArithmeticInterpreter intr = new ArithmeticInterpreter();
        //        intr.Result(result);
        //        ActionList.AddRange(intr.ActionList);

        //    }


        //}

        


    }


    public class OpStack
    {
        // behaves like a queue in some respects and a stack in others

        int _index;
        Dictionary<int, dynamic> _stack = new Dictionary<int, dynamic>();



        #region PUSHES

        /// <summary>
        /// adds a value at the specified index 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>

        public int PushAt(int index, dynamic value)
        {
 
            _stack.Add(index, value);
            _index++;
            return _index - 1;

        }


        /// <summary>
        /// adds a value at the next available index 
        /// </summary>
        /// <param name="value">
        /// 
        /// </param>
        public int Push(dynamic value)
        {

            _stack.Add(_index, value);
            _index++;
            return _index - 1;
        }

        /// <summary>
        /// adds a value at the specified index 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="AddAtIndex"></param>
        public void PushTop(dynamic value, int AddAtIndex)
        {
            // clar and add to temp
            Dictionary<int, dynamic> _temp = new Dictionary<int, dynamic>();
            var _key = _stack.Keys.Min();
            var insert_at = _key + AddAtIndex;


            while (_stack.Count > 0)
            {

                if (insert_at == _key)
                {
                    _key++;
                    _temp.Add(_key, value);
                }

                _key++;
                _temp.Add(_key, PopTop());
            }
            _stack = _temp;


        }


        public void PushRipple(int RippleIndex, int growthFactor)
        {
            if (this.Count() == 0) { return; }


            OpStack _temp = new OpStack();

            #region SPREAD
            // HERE WE TAKE EVERYTHING ABOVE THE "RippleIndex" and Push it to a temp table with new keys pushed farther apart
            var next = this.PeekBottomKey();
            do
            {
                _temp.PushAt(next + growthFactor, this.PopAt(next));
                next = this.PeekBottomKey();
            } while (next > RippleIndex);
            #endregion

            #region GATHER
            next = _temp.PeekBottomKey();
            var min = -1;
            do
            {
               

                this.PushAt(next, _temp.PopAt(next));

                if (_temp.Count() > 0)
                {
                    next = _temp.PeekBottomKey();
                    min = _temp._stack.Keys.Min();
                }
                else
                {
                    next = -1;
                }
                

            } while (next >= min);

            #endregion





        }


        #endregion

        #region PEEKS

        /// <summary>
        /// finds the value of a given entry given a key 
        /// </summary>
        /// <param name="key">
        /// 
        /// </param>

        public dynamic PeekValueAt(int key)
        {
            if (key > -1)
                return _stack[key];
            else
                return "";// instead of returning null, we do this so we can check it as a string without issue
        }
        
        public dynamic PeekSearchNext(params string[] str)
        {
            foreach (var _key in _stack.Keys)
            {
                var value = _stack[_key];

                foreach (string s in str)
                {
                    if (value.ToString() == s) { return _key; }
                }


            }


            return -1;
        }

        public dynamic PeekSearchAbove(int key, string str)
        {
            // here we don't want to search the entire set of keys, just those above our current key
            // so we have to figure out what the last key is and not try to search past that
            if (this.Count() > 1)
            {
                int _key = key;
                do
                {
                    _key = PeekSearchKeyPrevious(_key);
                    var value = _stack[_key];
                    if (value.ToString() == str) { return _key; }

                } while (_key > -1);
            }
            return -1;
        }

        public int PeekSearchKeyPrevious(int key)
        {
            this.Order();
            int _last_key_Searched = -1;
            foreach (var _key in _stack.Keys)
            {

                if (_key == key)
                {
                    return _last_key_Searched;
                }
                _last_key_Searched = _key;

            }

            return -1;
        }

        public dynamic PeekBottom()
        {
            // sequnce contains no elements is a good error here. we might hit this if we apply and the function returns nothing
            var _key = _stack.Keys.Max();
            var value = _stack[_key];

            return value;
        }

        public int PeekBottomKey()
        {
            var _key = _stack.Keys.Max();


            return _key;
        }


        #endregion

        #region POPS
        public dynamic PopTop()
        {
            var _key = _stack.Keys.Min();
            var value = _stack[_key];
            _stack.Remove(_key);

            return value;
        }


        public dynamic PopBottomPeek()
        {
            var _key = _stack.Keys.Max();
            var value = _stack[_key];
            _stack.Remove(_key);

            return PeekBottom();
        }

        public dynamic PopBottom()
        {
            var _key = _stack.Keys.Max();
            var value = _stack[_key];
            _stack.Remove(_key);

            return value;
        }

        public dynamic PopAt(int _key)
        {
            if (_key < 0) { return null; }

            var value = _stack[_key];
            _stack.Remove(_key);

            return value;
        }


        #endregion

        public  OpStack PopRange(int _key_start, int _keyend)
        {
            var _op = new OpStack();

            var iterator = 0;
            do {
                var value = this.PopAt(_key_start+ iterator);
                _op.PushAt(_key_start+ iterator, value);
                iterator++;
            }
            while (_key_start + iterator <= _keyend);
            return _op;
        }



        public void Clear()
        {
            _stack.Clear();

        }

        public int FirstValue(int key1, int key2)
        {
            // this fn is really only used for the iph
            int value = 0;

            if (key1 > -1 && key2 > -1) //both present
            {
                value = key1 > key2 ? key2 : key1;
            }

            if (key1 == -1 && key2 > -1) //both present
            {
                value = key2;
            }

            if (key1 > -1 && key2 == -1) //both present
            {
                value = key1;
            }

            return value;
        }




        public void Reverse()
        {

            int counter = 0;
            int to = this.Count();
            do
            {
                if (to > 1)
                {
                    var value = this.PopBottom();
                    this.PushTop(value, counter);                    
                }
                counter++;

            } while (counter < to-1);//[to-1] : the last one never needs to be swapped due to the way this works



        }

        public void Order()
        {
            Dictionary<int, dynamic> _temp = new Dictionary<int, dynamic>();
            int counter = 0;
            int to = this.Count();
            do
            {
                var _key = _stack.Keys.Min();
                var value = _stack[_key];
                var a = this.PopAt(_key);
                _temp.Add(_key, a);
                counter++;

            } while (counter < to);

            _stack = _temp;
        }
        

        public dynamic ReplaceBottom(dynamic value)
        {
            var _key = _stack.Keys.Max();
            _stack[_key] = value;

            return value;
        }

        public void RemoveAt(int key)
        {

            _stack.Remove(key);

           
        }

        public dynamic ReplaceKey(int key, dynamic value)
        {
           
            _stack[key] = value;

            return value;
        }

        public IEnumerator<dynamic> GetEnumerator()
        {

            while (_stack.Count > 0)
            {
                yield return PopTop();
            }
        }

        public dynamic TupleBuilder()
        {

        


            var items = from pair in _stack
                        orderby pair.Key ascending
                        select pair.Value;

            var _return_value = String.Join("|", items.ToArray());
            _return_value = "{" + _return_value + "}";
            return _return_value;
        }

        public int Count()
        {
            return _stack.Count();

        }


        public OpStack Copy()
        {
            var _newOP = new OpStack();
            _newOP._index = this._index;
            _newOP._stack = this._stack.ToDictionary(i => i.Key, i => i.Value); ;
           
            return _newOP;
        }
    

    }


    

    public class Arithmetic_ActionData
    {
        public dynamic  event_data{ get; }


        public Arithmetic_ActionData(dynamic _event_data)
        {
            event_data = _event_data;
        }

    }
    // no Event Data needed for this




}
