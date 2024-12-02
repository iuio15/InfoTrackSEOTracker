import React from "react";
import PropTypes from "prop-types";

const SearchHistoryTable = ({ history }) => (
    <table className="history-table">
        <thead>
            <tr>
                <th>Keyword</th>
                <th>URL</th>
                <th>Search Engine</th>
                <th>Positions</th>
                <th>Timestamp</th>
            </tr>
        </thead>
        <tbody>
            {history.map((entry, index) => (
                <tr key={index}>
                    <td>{entry.keyword}</td>
                    <td>{entry.url}</td>
                    <td>{entry.searchEngine}</td> 
                    <td>{entry.positions}</td>
                    <td>{new Date(entry.timestamp).toLocaleString()}</td>
                </tr>
            ))}
        </tbody>
    </table>
);

SearchHistoryTable.propTypes = {
    history: PropTypes.arrayOf(
        PropTypes.shape({
            keyword: PropTypes.string.isRequired,
            url: PropTypes.string.isRequired,
            searchEngine: PropTypes.string.isRequired,
            positions: PropTypes.string.isRequired,
            timestamp: PropTypes.string.isRequired,
        })
    ).isRequired,
};

export default SearchHistoryTable;