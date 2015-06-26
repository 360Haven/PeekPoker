using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using PeekPoker.Interface;

namespace PeekPoker.Search
{
    public partial class Search : Form
    {
        private readonly RealTimeMemory _rtm;
        private string _id;
        private string _length;
        private RwStream _readWriter;
        private BindingList<SearchResults> _searchLimitedResult = new BindingList<SearchResults>();
        private BindingList<SearchResults> _searchResult = new BindingList<SearchResults>();

        // temporary position and length for refreshing purpose
        private string _tempLength;

        private string _tempOffset;

        public Search(RealTimeMemory rtm)
        {
            InitializeComponent();
            _rtm = rtm;
            resultGrid.DataSource = _searchLimitedResult;
        }

        public event ShowMessageBoxHandler ShowMessageBox;

        public event UpdateProgressBarHandler UpdateProgressbar;

        public event EnableControlHandler EnableControl;

        public event GetTextBoxTextHandler GetTextBoxText;

        //Control changes
        private void GridRowColours(int value)
        {
            if (resultGrid.InvokeRequired)
                resultGrid.Invoke((MethodInvoker) (() => GridRowColours(value)));
            else
                resultGrid.Rows[value].DefaultCellStyle.ForeColor = Color.Red;
        }

        private void SearchRangeButtonClick(object sender, EventArgs e)
        {
            try
            {
                _tempLength = GetTextBoxText(lengthRangeAddressTextBox);
                _tempOffset = GetTextBoxText(startRangeAddressTextBox);
                var oThread = new Thread(SearchRange);
                oThread.Start();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Refresh results Thread
        private void RefreshResultList()
        {
            try
            {
                EnableControl(resultRefreshButton, false);
                var newSearchResults = new BindingList<SearchResults>();
                var limitSearchResults = new BindingList<SearchResults>();
                int value = 0;
                string retvalue = "";

                if (_searchResult.Count > 500)
                {
                    string results = _rtm.Peek(_tempOffset, _tempLength, _tempOffset, _tempLength);

                    _readWriter = new RwStream();
                    try
                    {
                        byte[] _buffer = Functions.HexToBytes(results);
                        _readWriter.WriteBytes(_buffer, 0, _buffer.Length);

                        _readWriter.Flush();
                        _readWriter.Position = 0;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

                foreach (SearchResults item in _searchResult)
                {
                    UpdateProgressbar(0, _searchResult.Count, value, "Refreshing...");

                    string length = (item.Value.Length/2).ToString("X");
                    if (_searchResult.Count > 500)
                    {
                        //var retvalue = this._rtm.
                        uint pos = uint.Parse(item.Offset, NumberStyles.AllowHexSpecifier);
                        uint spos = uint.Parse(_tempOffset, NumberStyles.AllowHexSpecifier);
                        _readWriter.Position = (pos - spos);
                        byte[] _value = _readWriter.ReadBytes((item.Value.Length/2));
                        retvalue = Functions.ToHexString(_value);
                    }
                    else
                    {
                        retvalue = _rtm.Peek(item.Offset, length, item.Offset, length);
                    }

                    uint currentResults;
                    uint newResult;

                    if (!uint.TryParse(item.Value, out currentResults))
                        throw new Exception("Invalid Search Value this function only works for Unsigned Integers.");
                    uint.TryParse(retvalue, out newResult);

                    //===================================================
                    //Default
                    if (defaultRadioButton.Checked)
                    {
                        if (item.Value == retvalue) continue; //if value hasn't change continue for each loop
                        if (value < 1000)
                        {
                            GridRowColours(value);
                        }
                        item.Value = retvalue;
                    }
                    else if (ifEqualsRadioButton.Checked)
                    {
                        if (newResult == currentResults)
                        {
                            var searchResultItem = new SearchResults
                                                       {
                                                           ID = item.ID,
                                                           Offset = item.Offset,
                                                           Value = retvalue
                                                       };
                            newSearchResults.Add(searchResultItem);
                        }
                    }
                    else if (ifGreaterThanRadioButton.Checked)
                    {
                        if (newResult > currentResults)
                        {
                            var searchResultItem = new SearchResults
                                                       {
                                                           ID = item.ID,
                                                           Offset = item.Offset,
                                                           Value = retvalue
                                                       };
                            newSearchResults.Add(searchResultItem);
                        }
                    }
                    else if (ifLessThanRadioButton.Checked)
                    {
                        if (newResult < currentResults)
                        {
                            var searchResultItem = new SearchResults
                                                       {
                                                           ID = item.ID,
                                                           Offset = item.Offset,
                                                           Value = retvalue
                                                       };
                            newSearchResults.Add(searchResultItem);
                        }
                    }
                    else if (ifLessThanRadioButton.Checked)
                    {
                        if (newResult != currentResults)
                        {
                            var searchResultItem = new SearchResults
                                                       {
                                                           ID = item.ID,
                                                           Offset = item.Offset,
                                                           Value = retvalue
                                                       };
                            newSearchResults.Add(searchResultItem);
                        }
                    }
                    else if (ifChangeRadioButton.Checked)
                    {
                        if (item.Value != retvalue)
                        {
                            var searchResultItem = new SearchResults
                                                       {
                                                           ID = item.ID,
                                                           Offset = item.Offset,
                                                           Value = retvalue
                                                       };
                            newSearchResults.Add(searchResultItem);
                        }
                    }

                    value++;
                }
                if (defaultRadioButton.Checked)
                {
                    ResultGridUpdate();
                    ResultCountBoxUpdate();
                }
                else
                {
                    _searchResult = newSearchResults;
                    for (int i = 0; i < _searchResult.Count; i++)
                    {
                        if (i >= 1000)
                            break;

                        limitSearchResults.Add(_searchResult[i]);
                    }

                    _searchLimitedResult = limitSearchResults;
                    ResultGridUpdate();
                    ResultCountBoxUpdate();
                }
                UpdateProgressbar(0, 100, 0, "idle");
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControl(resultRefreshButton, true);
                UpdateProgressbar(0, 100, 0, "idle");
                Thread.CurrentThread.Abort();
            }
        }

        // Refresh results
        private void ResultRefreshClick(object sender, EventArgs e)
        {
            if (_searchResult.Count > 0)
            {
                var thread = new Thread(RefreshResultList);
                thread.Start();
            }
            else
            {
                ShowMessageBox("Can not refresh! \r\n Result list empty!!", string.Format("Peek Poker"),
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResultGridCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var cell = (DataGridCell) sender;
            if (resultGrid.Rows[cell.RowNumber].Cells[2].Value != null)
                resultGrid.Rows[cell.RowNumber].DefaultCellStyle.ForeColor = Color.Red;
        }

        private void SearchRangeValueTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return || !searchRangeValueTextBox.Focused) return;
            var oThread = new Thread(SearchRange);
            oThread.Start();
            e.Handled = true;
            searchRangeButton.Focus();
        }

        //Searches the memory for the specified value (Experimental)
        private void SearchRange()
        {
            try
            {
                EnableControl(searchRangeButton, false);
                EnableControl(stopSearchButton, true);
                _rtm.DumpOffset = Functions.Convert(GetTextBoxText(startRangeAddressTextBox));
                _rtm.DumpLength = Functions.Convert(GetTextBoxText(lengthRangeAddressTextBox));

                ResultGridClean(); //Clean list view

                //The ExFindHexOffset function is a Experimental search function
                BindingList<SearchResults> results = _rtm.FindHexOffset(GetTextBoxText(searchRangeValueTextBox));
                //pointer
                //Reset the progressbar...
                UpdateProgressbar(0, 100, 0);

                if (results.Count < 1)
                {
                    ShowMessageBox(string.Format("No result/s found!"), string.Format("Peek Poker"),
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; //We don't want it to continue
                }

                _searchResult = results;
                var newLimitResult = new BindingList<SearchResults>();

                for (int i = 0; i < _searchResult.Count; i++)
                {
                    if (i >= 1000)
                        break;

                    newLimitResult.Add(_searchResult[i]);
                }
                _searchLimitedResult = newLimitResult;
                ResultGridUpdate();
                ResultCountBoxUpdate();
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControl(searchRangeButton, true);
                EnableControl(stopSearchButton, false);
                Thread.CurrentThread.Abort();
            }
        }

        //Refresh the values of Search Results
        private void ResultGridClean()
        {
            if (resultGrid.InvokeRequired)
                resultGrid.Invoke((MethodInvoker) (ResultGridClean));
            else
                resultGrid.Rows.Clear();
        }

        private void ResultGridUpdate()
        {
            //IList or represents a collection of objects(String)
            if (resultGrid.InvokeRequired)
                //lambda expression empty delegate that calls a recursive function if InvokeRequired
                resultGrid.Invoke((MethodInvoker) (ResultGridUpdate));
            else
            {
                resultGrid.DataSource = _searchLimitedResult;
                resultGrid.Refresh();
            }
        }

        //ResultCountBox
        private void ResultCountBoxUpdate()
        {
            //IList or represents a collection of objects(String)
            if (ResultCountBox.InvokeRequired)
                //lambda expression empty delegate that calls a recursive function if InvokeRequired
                ResultCountBox.Invoke((MethodInvoker) (ResultCountBoxUpdate));
            else
            {
                ResultCountBox.Text = _searchResult.Count.ToString();
                ResultCountBox.Refresh();
            }
        }

        private void StopSearchButtonClick(object sender, EventArgs e)
        {
            _rtm.StopSearch = true;
        }

        private void FixTheAddresses(object sender, EventArgs e)
        {
            try
            {
                if (!Functions.IsHex(startRangeAddressTextBox.Text))
                {
                    if (!startRangeAddressTextBox.Text.Equals(""))
                        startRangeAddressTextBox.Text = uint.Parse(startRangeAddressTextBox.Text).ToString("X");
                }

                if (!Functions.IsHex(lengthRangeAddressTextBox.Text))
                {
                    if (!lengthRangeAddressTextBox.Text.Equals(""))
                        lengthRangeAddressTextBox.Text = uint.Parse(lengthRangeAddressTextBox.Text).ToString("X");
                }

                uint value = Convert.ToUInt32(startRangeAddressTextBox.Text, 16);
                uint valueTwo = Convert.ToUInt32(lengthRangeAddressTextBox.Text, 16);
                totalTextBoxText.Text = (value + valueTwo).ToString("X");
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, "PeekNPoke", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchRangeValueTextBoxLeave(object sender, EventArgs e)
        {
            searchRangeValueTextBox.Text = searchRangeValueTextBox.Text.Replace(" ", "");
        }

        private void resultGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(string.Format("" + resultGrid.Rows[resultGrid.SelectedRows[0].Index].Cells[1].Value));
                e.SuppressKeyPress = true;
            }
        }

        private void ResultCopy(object sender, EventArgs e)
        {
            if (resultGrid.Rows.Count == 0)
                return;
            Clipboard.SetText(string.Format("" + resultGrid.Rows[resultGrid.SelectedRows[0].Index].Cells[1].Value));
        }

        private void searchRangeValueTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
            {
                try
                {
                    var oThread = new Thread(SearchRange);
                    oThread.Start();
                }
                catch (Exception ex)
                {
                    ShowMessageBox(ex.Message, string.Format("Peek Poker"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}