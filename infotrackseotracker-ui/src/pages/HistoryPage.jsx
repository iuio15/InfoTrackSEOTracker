import React, { useEffect, useState } from "react";
import SearchHistoryTable from "../components/SearchHistoryTable";
import PropTypes from "prop-types";
import '../styles/HistoryPage.css';

const HistoryPage = () => {
    const [history, setHistory] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchHistory = async () => {
            try {
                const response = await fetch("http://localhost:5000/api/search/history");
                if (!response.ok) {
                    throw new Error("Failed to fetch history.");
                }
                const data = await response.json();
                setHistory(data.reverse()); 
            } catch (err) {
                setError(err.message);
            } finally {
                setIsLoading(false);
            }
        };

        fetchHistory();
    }, []);

    if (isLoading) {
        return <p>Loading history...</p>;
    }

    if (error) {
        return <p className="error-message">{error}</p>;
    }

    return (
        <div className="history-page">
            <h2>Search History</h2>
            {history.length > 0 ? (
                <SearchHistoryTable history={history} />
            ) : (
                <p>No search history available.</p>
            )}
        </div>
    );
};

HistoryPage.propTypes = {
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

export default HistoryPage;